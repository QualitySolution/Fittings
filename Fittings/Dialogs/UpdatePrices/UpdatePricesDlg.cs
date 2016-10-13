using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.IO;
using System.Linq;
using Fittings.Domain;
using Gamma.GtkWidgets;
using Gamma.Utilities;
using Gtk;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using QSOrmProject;
using QSProjectsLib;
using QSWidgetLib;

namespace Fittings
{
	public partial class UpdatePricesDlg : QSTDI.TdiTabBase, IOrmDialog
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		IUnitOfWork uow = UnitOfWorkFactory.CreateWithoutRoot();

		string filePath;

		XSSFWorkbook wb;
		XSSFSheet sh;
		String Sheet_name;

		int maxColumns;

		List<UpdatingXLSRow> xlsRows;
		GenericObservableList<UpdatingXLSRow> ObservablexlsRows;

		Dictionary<UpdatingXLSRow.ColumnType, int> dataColumnsMap = new Dictionary<UpdatingXLSRow.ColumnType, int>();

		Menu SetColumnTypeMenu;
		int popupHeaderMenuColumnId;

		public IUnitOfWork UoW{get{ return uow;}}

		public object EntityObject{get{ return null;}}

		public UpdatePricesDlg()
		{
			logger.Info("Выбор файла для обновления цен...");

			if(SelectFile())
			{
				FailInitialize = true;
				return;
			}

			this.Build();

			TabName = "Расположение колонок (Шаг 1)";

			try
			{
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				{
					wb = new XSSFWorkbook(fs);
				}
			}
			catch(IOException ex)
			{
				if (ex.HResult == -2147024864)
				{
					MessageDialogWorks.RunErrorDialog("Указанный файл уже открыт в другом приложении. Оно заблокировало доступ к файлу.");
					FailInitialize = true;
					return;
				}
				throw ex;
			}

			comboCurrency.ItemsEnum = typeof(PriceСurrency);

			var sheets = new List<string>();
			for(int s = 0; s < wb.NumberOfSheets; s++)
			{
				sheets.Add(wb.GetSheetName(s));
			}
			comboSheet.ItemsList = sheets;

			ytreeviewSetColumns.EnableGridLines = Gtk.TreeViewGridLines.Both;
			ytreeviewSetColumns.HeadersClickable = true;
			SetColumnTypeMenu = GtkMenuHelper.GenerateMenuFromEnum<UpdatingXLSRow.ColumnType>(OnSetColumnHeaderMenuSelected);
			notebook1.ShowTabs = false;

			ytreeviewParsing.EnableGridLines = TreeViewGridLines.Both;
			ytreeviewParsing.Selection.Mode = SelectionMode.Multiple;
			ytreeviewParsing.Selection.Changed += YtreeviewParsing_Selection_Changed;

			comboProvider.SetRenderTextFunc<Provider>(x => x.Name);
		}

		#region Шаг 0

		/// <returns><c>true</c>, если пользователь отменил выбор файла.</returns>
		private bool SelectFile()
		{
			using (FileChooserDialog Chooser = new FileChooserDialog("Выберите фаил в котором нужно обновить цены...", 
				(Window)MainClass.MainWin.Toplevel,
				FileChooserAction.Open,
				"Отмена", ResponseType.Cancel,
				"Открыть", ResponseType.Accept))
			{
				FileFilter Filter = new FileFilter();
				Filter.AddPattern("*.xls");
				Filter.AddPattern("*.xlsx");
				Filter.Name = "Все поддерживаемые";
				Chooser.AddFilter(Filter);

				Filter = new FileFilter();
				Filter.AddPattern("*.xls");
				Filter.Name = "Excel 2003";
				Chooser.AddFilter(Filter);

				Filter = new FileFilter();
				Filter.AddPattern("*.xlsx");
				Filter.Name = "Excel 2007";
				Chooser.AddFilter(Filter);

				if ((ResponseType)Chooser.Run() == ResponseType.Accept)
				{
					filePath = Chooser.Filename;
				}

				Chooser.Destroy();
				return String.IsNullOrEmpty(filePath);
			}
		}

		#endregion

		#region Шаг1

		private void LoadSheet()
		{
			// get sheet
			logger.Info("Читаем лист...");
			xlsRows = new List<UpdatingXLSRow>();

			maxColumns = 0;

			int i = yspinSkipRows.ValueAsInt;
			while (sh.GetRow(i) != null)
			{
				xlsRows.Add(new UpdatingXLSRow(sh.GetRow(i)));
				maxColumns = Math.Max(sh.GetRow(i).Cells.Count, maxColumns);
				i++;
			}
			RefreshReadingColumns(maxColumns);
			ytreeviewSetColumns.ItemsDataSource = xlsRows;
			logger.Info("Ок");
		}

		void TryGuessParameters()
		{
			logger.Info("Пробуем определить расположение колонок...");
			dataColumnsMap.Clear();
			int i = sh.FirstRowNum;
			int skipRow = 0;
			while (sh.GetRow(i) != null)
			{
				var row = sh.GetRow(i);
				var newHeader = new Dictionary<UpdatingXLSRow.ColumnType, int>();
				for(int c = 0; c < row.Cells.Count; c++)
				{
					if (row.Cells[c].CellType != NPOI.SS.UserModel.CellType.String)
						continue;
					var value = row.Cells[c].StringCellValue.ToLower();
					if (value.Contains("dn"))
						newHeader[UpdatingXLSRow.ColumnType.DN] = c;
					if(value.Contains("pn"))
						newHeader[UpdatingXLSRow.ColumnType.PN] = c;
					if(value.Contains("price"))
					{
						newHeader[UpdatingXLSRow.ColumnType.Price] = c;
						if (value.Contains("rub") && value.Contains("rus"))
							comboCurrency.SelectedItem = PriceСurrency.RUB;
						if (value.Contains("usd"))
							comboCurrency.SelectedItem = PriceСurrency.USD;
						if (value.Contains("eur"))
							comboCurrency.SelectedItem = PriceСurrency.EUR;
					}
					if(value.Contains("model") || value.Contains("art."))
						newHeader[UpdatingXLSRow.ColumnType.Model] = c;
				}
				if(newHeader.Count > dataColumnsMap.Count)
				{
					dataColumnsMap = newHeader;
					skipRow = i + 1;
				}
				i++;
			}

			//Здесь вызовется метод LoadSheet
			yspinSkipRows.ValueAsInt = skipRow;
			if (dataColumnsMap.Count > 0)
			{
				RefrereshColumnsTitles();
				UpdateSetColumnStatus();
			}
		}

		protected void OnComboSheetChanged(object sender, EventArgs e)
		{
			if (wb == null)
				return;

			if(comboSheet.ActiveText != Sheet_name)
			{
				Sheet_name = comboSheet.ActiveText;
				sh = (XSSFSheet)wb.GetSheet(Sheet_name);
				TryGuessParameters();
				LoadSheet();
			}
		}

		void RefreshReadingColumns(int columnsCount)
		{
			if (ytreeviewSetColumns.Columns.Length == columnsCount)
				return;

			var config = ColumnsConfigFactory.Create<UpdatingXLSRow>();
			for(int i = 0; i < columnsCount; i++)
			{
				int col = i;
				config.AddColumn(getColumnNameFromIndex(i)).HeaderAlignment(0.5f)
					.ClickedEvent(OnSetColumnHeaderClicked)
					.AddTextRenderer(x => x.ToString(col));
			}

			ytreeviewSetColumns.ColumnsConfig = config.Finish();
		}

		void UpdateSetColumnStatus()
		{
			var text = "Кликните по заголовку колонки чтобы указать ее тип.";
			var skipedColumns = new List<string>();
			if (!dataColumnsMap.ContainsKey(UpdatingXLSRow.ColumnType.Price))
				skipedColumns.Add("<b>Цена</b>");
			if (!dataColumnsMap.ContainsKey(UpdatingXLSRow.ColumnType.Model))
				skipedColumns.Add("<b>Модель</b>");
			if (!dataColumnsMap.ContainsKey(UpdatingXLSRow.ColumnType.DN))
				skipedColumns.Add("<b>DN</b>");

			if(skipedColumns.Count > 0)
				text += String.Format(
					"\n<span foreground=\"red\">Клонки: {0} должны быть указаны.</span>",
					String.Join(", ", skipedColumns)
				);
			labelSetColumnsInfo.Markup = text;
			buttonNext.Sensitive = skipedColumns.Count == 0;
		}

		private static String getColumnNameFromIndex(int column)
		{
			String col = Convert.ToString((char)('A' + (column % 26)));
			while (column >= 26)
			{
				column = (column / 26) -1;
				col = Convert.ToString((char)('A' + (column % 26))) + col;
			}
			return col;
		}

		protected void OnSetColumnHeaderClicked(object sender, EventArgs e)
		{
			var col = sender as Gtk.TreeViewColumn;
			popupHeaderMenuColumnId = Array.FindIndex(ytreeviewSetColumns.Columns, x => x == col);
			SetColumnTypeMenu.Popup ();
		}
			
		protected void OnSetColumnHeaderMenuSelected(object sender, EventArgs e)
		{
			var item = sender as MenuItemId<UpdatingXLSRow.ColumnType>;
			dataColumnsMap[item.ID] = popupHeaderMenuColumnId;
			RefrereshColumnsTitles();
			UpdateSetColumnStatus();
		}

		private void RefrereshColumnsTitles()
		{
			for(int i = 0; i < ytreeviewSetColumns.Columns.Length; i++)
			{
				if (dataColumnsMap.ContainsValue(i))
					ytreeviewSetColumns.Columns[i].Title = dataColumnsMap.First(x => x.Value == i).Key.GetEnumTitle();
				else
					ytreeviewSetColumns.Columns[i].Title = getColumnNameFromIndex(i);
			}
		}

		protected void OnYspinSkipRowsValueChanged(object sender, EventArgs e)
		{
			LoadSheet();
		}

		#endregion

		#region Шаг 2

		void PrepareData()
		{
			progressParsing.Text = "Подготовка таблицы";
			progressParsing.Adjustment.Value = 0;
			logger.Info("Подготовка таблицы");
			//Устанавливаем раскладку по колонкам
			xlsRows.ForEach(x => x.ColumnsMap = dataColumnsMap);
			RefreshParsingColumns();

			var currency = (PriceСurrency)comboCurrency.SelectedItem;

			progressParsing.Text = "Формирование справочных данных";
			logger.Info("Формирование справочных данных");
			progressParsing.Adjustment.Value++;
			var wc = new UpdatingXLSWorkClass();
			wc.UoW = uow;
			wc.Diameters = uow.GetAll<Diameter>().ToList();
			wc.Currency = currency;

			xlsRows.ForEach(x => x.WC = wc);

			ObservablexlsRows = new GenericObservableList<UpdatingXLSRow>(xlsRows);
			ObservablexlsRows.ListContentChanged += ObservablexlsRows_ListContentChanged;
			ytreeviewParsing.ItemsDataSource = ObservablexlsRows;

			progressParsing.Text = "Разбор данных";
			progressParsing.Adjustment.Value++;
			progressParsing.Adjustment.Upper = xlsRows.Count + 2;
			logger.Info("Разбор данных");

			foreach(var row in xlsRows)
			{
				row.TryParse();
				progressParsing.Adjustment.Value++;
				QSMain.WaitRedraw();
			}

			logger.Info("Ок");
			progressParsing.Text = String.Empty;
			progressParsing.Adjustment.Value = 0;
		}

		void RefreshParsingColumns()
		{
			var config = ColumnsConfigFactory.Create<UpdatingXLSRow>();
			for(int i = 0; i < maxColumns; i++)
			{
				int col = i;
				string columnTitle;

				if(dataColumnsMap[UpdatingXLSRow.ColumnType.Price] == col)
				{
					config.AddColumn("Статус").AddTextRenderer(x => x.Status.GetEnumTitle())
						.AddSetter((w, x) => w.Foreground = GetColorByStatus(x.Status));
					columnTitle = "Цена в файле";
				}
				else if (dataColumnsMap.ContainsValue(i))
					columnTitle = dataColumnsMap.First(x => x.Value == i).Key.GetEnumTitle();
				else
					columnTitle = getColumnNameFromIndex(i);

				config.AddColumn(columnTitle).HeaderAlignment(0.5f)
					.AddTextRenderer(x => x.ToString(col));

				if(dataColumnsMap[UpdatingXLSRow.ColumnType.Price] == col)
				{
					config.AddColumn("Изм.").AddToggleRenderer(x => x.ChangePrice).AddSetter((w, x) => w.Activatable = x.CanChangePrice)
						.AddColumn("Новая цена").AddTextRenderer(x => x.DisplayNewPrice)
						.AddColumn("Поставщик").AddTextRenderer(x => x.DisplayProvider);
				}
			}

			ytreeviewParsing.ColumnsConfig = config.Finish();
		}

		void ObservablexlsRows_ListContentChanged (object sender, EventArgs e)
		{
			if (notebook1.Page == 1)
				NextButtonStateUpdate();
		}

		string GetColorByStatus(UpdatingXLSRow.RowStatus status)
		{
			switch(status)
			{
				case UpdatingXLSRow.RowStatus.BadDiameter:
					return "red";
				case UpdatingXLSRow.RowStatus.AutoNewPrice:
				case UpdatingXLSRow.RowStatus.ManualSet:
					return "green";
				case UpdatingXLSRow.RowStatus.MultiFound:
					return "violet";
				case UpdatingXLSRow.RowStatus.NotFound:
				case UpdatingXLSRow.RowStatus.OnlyModelFound:
					return "blue";
				case UpdatingXLSRow.RowStatus.PriceNotChanged:
					return "lime";
				case UpdatingXLSRow.RowStatus.Skiped:
					return "gray";
				default:
					return "black";
			}
		}

		void YtreeviewParsing_Selection_Changed (object sender, EventArgs e)
		{
			var selectedList = ytreeviewParsing.GetSelectedObjects<UpdatingXLSRow>();

			if (selectedList.Any(x => x.IsMultiFound))
			{
				comboProvider.ItemsList = selectedList.Where(x => x.IsMultiFound).SelectMany(x => x.Prices).Select(x => x.Price.Provider).Distinct();
			}
			else
			{
				comboProvider.ItemsList = null;
			}
			buttonResolveMultiFound.Sensitive = false;

			CheckedButtonsStateUpdate();
		}

		private void CheckedButtonsStateUpdate()
		{
			var selectedList = ytreeviewParsing.GetSelectedObjects<UpdatingXLSRow>();
			buttonSelectAll.Sensitive = selectedList.Any(x => x.CanChangePrice && x.ChangePrice == false);
			buttonUnSelect.Sensitive = selectedList.Any(x => x.CanChangePrice && x.ChangePrice == true);
		}

		protected void OnButtonResolveMultiFoundClicked(object sender, EventArgs e)
		{
			var selectedList = ytreeviewParsing.GetSelectedObjects<UpdatingXLSRow>().Where(x => x.IsMultiFound);
			var selectedProvider = comboProvider.SelectedItem as Provider;
			foreach(var item in selectedList)
			{
				var newPrice = item.Prices.FirstOrDefault(x => x.Price.Provider.Id == selectedProvider.Id);
				if(newPrice != null)
				{
					item.SelectedPrice = newPrice;
					item.Status = UpdatingXLSRow.RowStatus.ManualSet;
					item.ChangePrice = item.CanChangePrice;
				}
			}
			CheckedButtonsStateUpdate();
		}

		protected void OnComboProviderChanged(object sender, EventArgs e)
		{
			buttonResolveMultiFound.Sensitive = comboProvider.SelectedItem != null;
		}

		protected void OnButtonSelectAllClicked(object sender, EventArgs e)
		{
			var selectedList = ytreeviewParsing.GetSelectedObjects<UpdatingXLSRow>();
			selectedList.Where(x => x.CanChangePrice && x.ChangePrice == false).ToList().ForEach(x => x.ChangePrice = true);
			CheckedButtonsStateUpdate();
		}

		protected void OnButtonUnSelectClicked(object sender, EventArgs e)
		{
			var selectedList = ytreeviewParsing.GetSelectedObjects<UpdatingXLSRow>();
			selectedList.Where(x => x.CanChangePrice && x.ChangePrice == true).ToList().ForEach(x => x.ChangePrice = false);
			CheckedButtonsStateUpdate();
		}

		private void NextButtonStateUpdate()
		{
			buttonNext.Sensitive = xlsRows.Any(x => x.ChangePrice);
		}

		#endregion

		#region Шаг 3

		void CalculateCompleteInfo()
		{
			labelTotal.LabelProp = xlsRows.Count.ToString();
			labelFind.LabelProp = xlsRows.Count(x => x.Status == UpdatingXLSRow.RowStatus.AutoNewPrice).ToString();
			labelManualSet.LabelProp = xlsRows.Count(x => x.Status == UpdatingXLSRow.RowStatus.ManualSet).ToString();
			labelPriceNotChanged.LabelProp = xlsRows.Count(x => x.Status == UpdatingXLSRow.RowStatus.PriceNotChanged).ToString();
			labelWillChangePrice.Markup = String.Format("<span foreground=\"green\">{0}</span>", xlsRows.Count(x => x.ChangePrice));

			labelCurrency.LabelProp = comboCurrency.ActiveText;
		}

		protected void OnButtonFinishClicked(object sender, EventArgs e)
		{
			string saveTo = null;
			progressFinal.Adjustment.Upper = 2;
			logger.Info("Пользователь выбирает файл...");
			using (FileChooserDialog Chooser = new FileChooserDialog("Выберите куда сохранить изменения...", 
				(Window)MainClass.MainWin.Toplevel,
				FileChooserAction.Save,
				"Отмена", ResponseType.Cancel,
				"Сохранить", ResponseType.Accept))
			{
				Chooser.SetFilename(filePath);
				Chooser.DoOverwriteConfirmation = true;

				FileFilter Filter = new FileFilter();
				Filter.AddPattern("*.xls");
				Filter.AddPattern("*.xlsx");
				Filter.Name = "Все поддерживаемые";
				Chooser.AddFilter(Filter);

				Filter = new FileFilter();
				Filter.AddPattern("*.xls");
				Filter.Name = "Excel 2003";
				Chooser.AddFilter(Filter);

				Filter = new FileFilter();
				Filter.AddPattern("*.xlsx");
				Filter.Name = "Excel 2007";
				Chooser.AddFilter(Filter);

				if ((ResponseType)Chooser.Run() == ResponseType.Accept)
				{
					saveTo = Chooser.Filename;
				}

				Chooser.Destroy();
			}
			if (String.IsNullOrEmpty(saveTo))
				return;

			progressFinal.Adjustment.Value++;
			logger.Info("Обновляем таблицы...");

			foreach(var row in xlsRows.Where(x => x.ChangePrice))
			{
				var priceCell = row.XlsRow.GetCell(row.ColumnsMap[UpdatingXLSRow.ColumnType.Price]);

				priceCell.SetCellValue((double)row.NewPrice.Value);
			}

			progressFinal.Adjustment.Value++;
			logger.Info("Записываем фаил...");
			try
			{
				using (FileStream file = new FileStream(saveTo, FileMode.Create, FileAccess.Write))
				{
					wb.Write(file);
				}
			}
			catch(IOException ex)
			{
				if (ex.HResult == -2147024864)
				{
					MessageDialogWorks.RunErrorDialog("Указанный файл уже открыт в другом приложении. Оно заблокировало доступ к файлу.");
					return;
				}
				throw ex;
			}

			progressFinal.Adjustment.Value++;
			if(checkOpenAfterSave.Active)
			{
				logger.Info("Открываем во внешем приложении...");
				System.Diagnostics.Process.Start (saveTo);
			}
			logger.Info("Ок");

			OnCloseTab(false);
		}

		#endregion

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			OnCloseTab (false);
		}

		protected void OnButtonNextClicked(object sender, EventArgs e)
		{
			switch(notebook1.Page)
			{
				case 0:
					TabName = "Обновляем цены (Шаг 2)";
					buttonPrev.Sensitive = true;
					notebook1.NextPage();
					PrepareData();
					break;
				case 1:
					TabName = "Сохраняем XLS (Шаг 3)";
					buttonPrev.Sensitive = true;
					buttonNext.Sensitive = false;
					notebook1.NextPage();
					CalculateCompleteInfo();
					break;
			}
		}

		protected void OnButtonPrevClicked(object sender, EventArgs e)
		{
			switch(notebook1.Page)
			{
				case 1:
					TabName = "Расположение колонок (Шаг 1)";
					buttonPrev.Sensitive = false;
					buttonNext.Sensitive = true;
					notebook1.PrevPage();
					break;
				case 2:
					TabName = "Обновляем цены (Шаг 2)";
					buttonPrev.Sensitive = true;
					buttonNext.Sensitive = true;
					notebook1.PrevPage();
					break;
			}
		}
	}
}


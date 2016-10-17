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
	public partial class PriceLoadDlg : QSTDI.TdiTabBase, IOrmDialog
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		IUnitOfWork uow = UnitOfWorkFactory.CreateWithoutRoot();

		public event EventHandler<PriceLoadCompletedEventArgs> PriceLoadCompleted;

		XSSFWorkbook wb;
		XSSFSheet sh;
		String Sheet_name;

		List<ReadingXLSRow> xlsRows;
		GenericObservableList<ReadingXLSRow> ObservablexlsRows;

		Dictionary<ReadingXLSRow.ColumnType, int> dataColumnsMap = new Dictionary<ReadingXLSRow.ColumnType, int>();

		Menu SetColumnTypeMenu;
		int popupHeaderMenuColumnId;

		public IUnitOfWork UoW{get{ return uow;}}

		public object EntityObject{get{ return null;}}

		public PriceLoadDlg(string filePath)
		{
			this.Build();

			TabName = "Загрузка прайса (Шаг 1)";

			try
			{
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				{
					wb = new XSSFWorkbook(fs);
				}
			}catch(IOException ex)
			{
				if (ex.HResult == -2147024864)
				{
					MessageDialogWorks.RunErrorDialog("Указанный файл уже открыт в другом приложении. Оно заблокировало доступ к файлу.");
					FailInitialize = true;
					return;
				}
				throw ex;
			}

			var sheets = new List<string>();
			for(int s = 0; s < wb.NumberOfSheets; s++)
			{
				sheets.Add(wb.GetSheetName(s));
			}
			comboSheet.ItemsList = sheets;

			ytreeviewSetColumns.EnableGridLines = Gtk.TreeViewGridLines.Both;
			ytreeviewSetColumns.HeadersClickable = true;
			SetColumnTypeMenu = GtkMenuHelper.GenerateMenuFromEnum<ReadingXLSRow.ColumnType>(OnSetColumnHeaderMenuSelected);
			notebook1.ShowTabs = false;

			//Вкладка второго экрана
			ytreeviewParsing.ColumnsConfig = ColumnsConfigFactory.Create<ReadingXLSRow>()
				.AddColumn("Статус").Resizable().AddTextRenderer(x => x.Status.GetEnumTitle())
				.AddSetter((w, x) => w.Foreground = GetColorByStatus(x.Status))
				.AddColumn ("Тип").Resizable().SetDataProperty (node => node.DispalyType)
				.AddColumn ("Диаметр").Resizable().SetDataProperty (node => node.DispalyDiameter)
				.AddColumn ("Давление").Resizable().SetDataProperty (node => node.DispalyPressure)
				.AddColumn ("Cоединения").Resizable().SetDataProperty (node => node.DispalyConnection)
				.AddColumn ("Материал").Resizable().SetDataProperty (node => node.DispalyMaterial)
				.AddColumn ("Артикул").Resizable().SetDataProperty (node => node.DispalyModel)
				.AddColumn("Цена").Resizable().AddNumericRenderer(x => x.Price)
				.AddSetter((w, x) => w.Background = x.Price.HasValue ? "white" : "red")
				.AddColumn("DN(XLS)").Resizable().AddTextRenderer(x => x.DNText).Background("White Smoke")
				.AddColumn("PN(XLS)").Resizable().AddTextRenderer(x => x.PNText).Background("White Smoke")
				.AddColumn("Модель(XLS)").Resizable().AddTextRenderer(x => x.ModelText).Background("White Smoke")
				.AddColumn("Цена(XLS)").Resizable().AddTextRenderer(x => x.PriceText).Background("White Smoke")
				.Finish();
			ytreeviewParsing.EnableGridLines = TreeViewGridLines.Both;
			ytreeviewParsing.Selection.Mode = SelectionMode.Multiple;
			ytreeviewParsing.Selection.Changed += YtreeviewParsing_Selection_Changed;
			comboCurrency.ItemsEnum = typeof(PriceСurrency);
		}
			
		#region Шаг1

		private void LoadSheet()
		{
			// get sheet
			logger.Info("Читаем лист...");
			xlsRows = new List<ReadingXLSRow>();

			int maxColumns = 0;

			int i = yspinSkipRows.ValueAsInt;
			while (sh.GetRow(i) != null)
			{
				xlsRows.Add(new ReadingXLSRow(sh.GetRow(i)));
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
			int i = 0;
			int skipRow = 0;
			while (sh.GetRow(i) != null)
			{
				var row = sh.GetRow(i);
				var newHeader = new Dictionary<ReadingXLSRow.ColumnType, int>();
				for(int c = 0; c < row.Cells.Count; c++)
				{
					if (row.Cells[c].CellType != NPOI.SS.UserModel.CellType.String)
						continue;
					var value = row.Cells[c].StringCellValue.ToLower();
					if (value.Contains("dn"))
						newHeader[ReadingXLSRow.ColumnType.DN] = c;
					if(value.Contains("pn"))
						newHeader[ReadingXLSRow.ColumnType.PN] = c;
					if(value.Contains("price"))
						newHeader[ReadingXLSRow.ColumnType.Price] = c;
					if(value.Contains("model"))
						newHeader[ReadingXLSRow.ColumnType.Model] = c;
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

			var config = ColumnsConfigFactory.Create<ReadingXLSRow>();
			for(int i = 0; i < columnsCount; i++)
			{
				int col = i;
				config.AddColumn(getColumnNameFromIndex(i)).HeaderAlignment(0.5f).Resizable()
					.ClickedEvent(OnSetColumnHeaderClicked)
					.AddTextRenderer(x => x.ToString(col));
			}

			ytreeviewSetColumns.ColumnsConfig = config.Finish();
		}

		void UpdateSetColumnStatus()
		{
			var text = "Кликните по заголовку колонки чтобы указать ее тип.";
			if (!dataColumnsMap.ContainsKey(ReadingXLSRow.ColumnType.Price))
				text += "\n<span foreground=\"red\">Клонка <b>Цена</b> должна быть указана.</span>";
			labelSetColumnsInfo.Markup = text;
			buttonNext.Sensitive = dataColumnsMap.ContainsKey(ReadingXLSRow.ColumnType.Price);
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
			var item = sender as MenuItemId<ReadingXLSRow.ColumnType>;
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
			ytreeviewParsing.Columns.First(x => x.Title == "DN(XLS)").Visible = dataColumnsMap.ContainsKey(ReadingXLSRow.ColumnType.DN);
			ytreeviewParsing.Columns.First(x => x.Title == "PN(XLS)").Visible = dataColumnsMap.ContainsKey(ReadingXLSRow.ColumnType.PN);
			ytreeviewParsing.Columns.First(x => x.Title == "Модель(XLS)").Visible = dataColumnsMap.ContainsKey(ReadingXLSRow.ColumnType.Model);

			ObservablexlsRows = new GenericObservableList<ReadingXLSRow>(xlsRows);
			ytreeviewParsing.ItemsDataSource = ObservablexlsRows;

			progressParsing.Text = "Формирование справочных данных";
			logger.Info("Формирование справочных данных");
			progressParsing.Adjustment.Value++;
			var wc = new ReadingXLSWorkClass();
			wc.UoW = uow;
			wc.Diameters = uow.GetAll<Diameter>().ToList();
			wc.Pressures = uow.GetAll<Pressure>().ToList();

			xlsRows.ForEach(x => x.WC = wc);

			progressParsing.Text = "Разбор данных";
			progressParsing.Adjustment.Value++;
			progressParsing.Adjustment.Upper = xlsRows.Count + 2;
			logger.Info("Разбор данных");

			foreach(var row in xlsRows)
			{
				row.TryParse();
				progressParsing.Adjustment.Value++;
				QSProjectsLib.QSMain.WaitRedraw();
			}
			logger.Info("Ок");
			progressParsing.Text = String.Empty;
			progressParsing.Adjustment.Value = 0;
		}

		string GetColorByStatus(ReadingXLSRow.RowStatus status)
		{
			switch(status)
			{
				case ReadingXLSRow.RowStatus.BadDiameter:
					return "red";
				case ReadingXLSRow.RowStatus.FoundModel:
				case ReadingXLSRow.RowStatus.ManualSet:
					return "green";
				case ReadingXLSRow.RowStatus.MultiFound:
					return "violet";
				case ReadingXLSRow.RowStatus.NotFound:
					return "blue";
				case ReadingXLSRow.RowStatus.WillCreated:
					return "lime";
				default:
					return "black";
			}
		}

		void YtreeviewParsing_Selection_Changed (object sender, EventArgs e)
		{
			var newSelected = ytreeviewParsing.GetSelectedObjects<ReadingXLSRow>().Any(x => x.Fitting == null);
			buttonMultiEdit.Sensitive = newSelected;

			bool oneRowSelected = ytreeviewParsing.Selection.CountSelectedRows() == 1;
			buttonManualSet.Sensitive = oneRowSelected;
			var oneRow = ytreeviewParsing.GetSelectedObjects<ReadingXLSRow>().FirstOrDefault();
			buttonResolveMultiFound.Sensitive = oneRowSelected && oneRow.IsMultiFound;
		}

		protected void OnButtonMultiEditClicked(object sender, EventArgs e)
		{
			var newSelected = ytreeviewParsing.GetSelectedObjects<ReadingXLSRow>().ToList();
			multiedit.StartEditing(newSelected);
		}

		protected void OnYtreeviewParsingRowActivated(object o, RowActivatedArgs args)
		{
			var row = ytreeviewParsing.GetSelectedObjects<ReadingXLSRow>().First();
			if (row.IsMultiFound)
				buttonResolveMultiFound.Click();
			else if (row.Status == ReadingXLSRow.RowStatus.BadDiameter || row.Status == ReadingXLSRow.RowStatus.NotFound || row.Status == ReadingXLSRow.RowStatus.WillCreated)
				buttonMultiEdit.Click();
			else if (row.Status == ReadingXLSRow.RowStatus.FoundModel || row.Status == ReadingXLSRow.RowStatus.ManualSet)
				buttonManualSet.Click();
		}

		protected void OnButtonResolveMultiFoundClicked(object sender, EventArgs e)
		{
			var editingRow = ytreeviewParsing.GetSelectedObjects<ReadingXLSRow> ().First();
			var filter = new FittingsFlt(UoW);
			filter.RestrictDiameter = editingRow.Diameter;
			filter.RestrictModel = editingRow.Code;
			var dlg = new ReferenceRepresentation (new Fittings.ViewModel.FittingsVM(filter));
			dlg.Tag = editingRow;
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += DlgMultiFoundResolve_ObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		void DlgMultiFoundResolve_ObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var dlg = sender as ReferenceRepresentation;
			var editingRow = dlg.Tag as ReadingXLSRow;
			editingRow.Fitting = UoW.GetById<Fitting>(e.ObjectId);
			editingRow.Status = ReadingXLSRow.RowStatus.ManualSet;
		}

		protected void OnButtonManualSetClicked(object sender, EventArgs e)
		{
			var editingRow = ytreeviewParsing.GetSelectedObjects<ReadingXLSRow> ().First();
			var dlg = new ReferenceRepresentation (new Fittings.ViewModel.FittingsVM());
			dlg.Tag = editingRow;
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += DlgManualSet_ObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		void DlgManualSet_ObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var dlg = sender as ReferenceRepresentation;
			var editingRow = dlg.Tag as ReadingXLSRow;
			editingRow.Fitting = UoW.GetById<Fitting>(e.ObjectId);
			editingRow.Status = ReadingXLSRow.RowStatus.ManualSet;
		}

		#endregion

		#region Шаг 3

		void CalculateCompleteInfo()
		{
			labelTotal.LabelProp = xlsRows.Count.ToString();
			labelFind.LabelProp = xlsRows.Count(x => x.Status == ReadingXLSRow.RowStatus.FoundModel).ToString();
			labelManualSet.LabelProp = xlsRows.Count(x => x.Status == ReadingXLSRow.RowStatus.ManualSet).ToString();
			labelCreate.LabelProp = xlsRows.Count(x => x.Status == ReadingXLSRow.RowStatus.WillCreated).ToString();
			var withoutPrice = xlsRows.Count(x => x.Price == null);
			if (withoutPrice > 0)
				labelWithoutPrice.Markup = String.Format("<span foreground=\"red\">{0}</span>", withoutPrice);
			else
				labelWithoutPrice.Markup = withoutPrice.ToString();

			var toAdd = xlsRows.Where(x => x.Price.HasValue).Count(x => x.Status == ReadingXLSRow.RowStatus.WillCreated
				|| x.Status == ReadingXLSRow.RowStatus.ManualSet
				|| x.Status == ReadingXLSRow.RowStatus.FoundModel
			            );
			var skiped = xlsRows.Count - toAdd;
			if (skiped > 0)
				labelSkiped.Markup = String.Format("<span foreground=\"red\">{0}</span>", skiped);
			else
				labelSkiped.Markup = skiped.ToString();
			labelCurrency.LabelProp = comboCurrency.ActiveText;

			buttonFinish.Label = RusNumber.FormatCase(toAdd, "Добавить {0} позицию в прайс", "Добавить {0} позиции в прайс", "Добавить {0} позиций в прайс");
			buttonFinish.Sensitive = toAdd > 0;
		}

		protected void OnButtonFinishClicked(object sender, EventArgs e)
		{
			progressFinal.Adjustment.Upper = 3;
			logger.Info("Создаем новую арматуру...");
			foreach(var row in xlsRows.Where(x => x.Status == ReadingXLSRow.RowStatus.WillCreated))
			{
				row.Fitting = new Fitting{
					Code = row.Code,
					BodyMaterial = row.BodyMaterial,
					ConnectionType = row.ConnectionType,
					Diameter = row.Diameter,
					DiameterUnits = row.DiameterUnits,
					Name = row.Name,
					Note = row.Note,
					Pressure = row.Pressure,
					PressureUnits = row.PressureUnits,
				};
				UoW.Save(row.Fitting);
			}
			progressFinal.Adjustment.Value++;
			logger.Info("Записываем в базу...");
			UoW.Commit();
			progressFinal.Adjustment.Value++;
			logger.Info("Передаем в диалог прайса...");
			if(PriceLoadCompleted != null)
			{
				var arg = new PriceLoadCompletedEventArgs{
					Rows = xlsRows.Where(x => x.Fitting != null && x.Price != null).ToArray(),
					Currency = (PriceСurrency)comboCurrency.SelectedItem
				};
				PriceLoadCompleted(this, arg);
			}
			progressFinal.Adjustment.Value++;
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
					TabName = "Загрузка прайса (Шаг 2)";
					buttonPrev.Sensitive = true;
					notebook1.NextPage();
					PrepareData();
					break;
				case 1:
					TabName = "Загрузка прайса (Шаг 3)";
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
					TabName = "Загрузка прайса (Шаг 1)";
					buttonPrev.Sensitive = false;
					buttonNext.Sensitive = true;
					notebook1.PrevPage();
					break;
				case 2:
					TabName = "Загрузка прайса (Шаг 2)";
					buttonPrev.Sensitive = true;
					buttonNext.Sensitive = true;
					notebook1.PrevPage();
					break;
			}
		}
	}

	public class PriceLoadCompletedEventArgs : EventArgs{
		public ReadingXLSRow[] Rows;
		public PriceСurrency Currency;
	}
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gtk;
using NPOI.XSSF.UserModel;
using QSOrmProject;
using QSWidgetLib;
using Gamma.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Fittings
{
	public partial class PriceLoadDlg : QSTDI.TdiTabBase
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		XSSFWorkbook wb;
		XSSFSheet sh;
		String Sheet_name;

		List<LoadingPriceItem> Items;
		List<ReadingXLSRow> xlsRows;
		Dictionary<ColumnDataType, int> dataColumnsMap = new Dictionary<ColumnDataType, int>();

		Menu SetColumnTypeMenu;
		int popupHeaderMenuColumnId;

		public PriceLoadDlg(string filePath)
		{
			this.Build();

			TabName = "Загрузка прайса (Шаг 1)";

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				wb = new XSSFWorkbook(fs);
			}

			var sheets = new List<string>();
			for(int s = 0; s < wb.NumberOfSheets; s++)
			{
				sheets.Add(wb.GetSheetName(s));
			}
			comboSheet.ItemsList = sheets;

			ytreeviewSetColumns.EnableGridLines = Gtk.TreeViewGridLines.Both;
			ytreeviewSetColumns.HeadersClickable = true;
			SetColumnTypeMenu = GtkMenuHelper.GenerateMenuFromEnum<ColumnDataType>(OnSetColumnHeaderMenuSelected);
		}

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
				var newHeader = new Dictionary<ColumnDataType, int>();
				for(int c = 0; c < row.Cells.Count; c++)
				{
					if (row.Cells[c].CellType != NPOI.SS.UserModel.CellType.String)
						continue;
					var value = row.Cells[c].StringCellValue.ToLower();
					if (value.Contains("dn"))
						newHeader[ColumnDataType.DN] = c;
					if(value.Contains("pn"))
						newHeader[ColumnDataType.PN] = c;
					if(value.Contains("price"))
						newHeader[ColumnDataType.Price] = c;
					if(value.Contains("model"))
						newHeader[ColumnDataType.Model] = c;
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

			var config = Gamma.GtkWidgets.ColumnsConfigFactory.Create<ReadingXLSRow>();
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
			if (!dataColumnsMap.ContainsKey(ColumnDataType.Price))
				text += "\n<span foreground=\"red\">Клонка <b>Цена</b> должна быть указана.</span>";
			labelSetColumnsInfo.Markup = text;
			buttonNext.Sensitive = dataColumnsMap.ContainsKey(ColumnDataType.Price);
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
			var item = sender as MenuItemId<ColumnDataType>;
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

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			OnCloseTab (false);
		}

		protected void OnYspinSkipRowsValueChanged(object sender, EventArgs e)
		{
			LoadSheet();
		}
	}

	public enum ColumnDataType{
		DN,
		PN,
		[Display(Name = "Модель")]
		Model,
		[Display(Name = "Цена")]
		Price
	}

	public class LoadingPriceItem
	{
		public string DNText { get; set;}
		public string PNText { get; set;}
		public string ModelText { get; set;}
		public string PriceText { get; set;}
	}

	public class ReadingXLSRow
	{
		public bool IsSetupRow = false;

		public NPOI.SS.UserModel.IRow XlsRow;

		public ReadingXLSRow(NPOI.SS.UserModel.IRow row)
		{
			XlsRow = row;
		}

		public ReadingXLSRow()
		{
			IsSetupRow = true;
		}

		public string ToString(int column)
		{
			var cell = XlsRow.GetCell(column);

			if (cell != null)
			{
				// TODO: you can add more cell types capatibility, e. g. formula
				switch (cell.CellType)
				{
					case NPOI.SS.UserModel.CellType.Numeric:
						return cell.NumericCellValue.ToString();
					case NPOI.SS.UserModel.CellType.String:
						return cell.StringCellValue;
				}
			}
			return null;
		}
	}
}


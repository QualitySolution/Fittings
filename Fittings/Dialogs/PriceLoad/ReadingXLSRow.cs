using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QSOrmProject;
using Fittings.Domain;
using NPOI.SS.UserModel;

namespace Fittings
{

	public class ReadingXLSRow : PropertyChangedBase
	{
		public NPOI.SS.UserModel.IRow XlsRow;

		public Dictionary<ColumnDataType, int> ColumnsMap;

		public decimal? Price { get; set; }

		ReadingXlsStatus status;
		public ReadingXlsStatus Status{
			get	{ return status;}
			set	{ SetField (ref status, value, () => Status);}
		}

		public bool IsMultiFound { get; private set;}

		public Fitting Fitting { get; set;}

		public ReadingXLSRow(NPOI.SS.UserModel.IRow row)
		{
			XlsRow = row;
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

		public void TryParse(ReadingXLSWorkClass wc)
		{
			//Парсим цену
			var priceCell = XlsRow.GetCell(ColumnsMap[ColumnDataType.Price]);
			if (priceCell.CellType == CellType.Numeric)
				Price = (decimal)priceCell.NumericCellValue;
			else if (priceCell.CellType == CellType.String)
			{
				decimal price;
				if (Decimal.TryParse(priceCell.StringCellValue, out price))
					Price = price;
			}
			else
				Price = null;

			//Парсим диаметр
			var dnCell = XlsRow.GetCell(ColumnsMap[ColumnDataType.DN]);
			if (dnCell.CellType == CellType.Numeric)
				wc.ParseDiameter(dnCell.NumericCellValue.ToString(), this);
			else if (dnCell.CellType == CellType.String)
				wc.ParseDiameter(dnCell.StringCellValue, this);

			if (Diameter == null)
			{
				Status = ReadingXlsStatus.BadDiameter;
			}

			//Парсим давление если есть
			if(ColumnsMap.ContainsKey(ColumnDataType.PN))
			{
				var pnCell = XlsRow.GetCell(ColumnsMap[ColumnDataType.PN]);
				if (pnCell.CellType == CellType.Numeric)
					wc.ParsePressure(pnCell.NumericCellValue.ToString(), this);
				else if (pnCell.CellType == CellType.String)
					wc.ParsePressure(pnCell.StringCellValue, this);
			}

			//Находим номенклатуру.
			if(ColumnsMap.ContainsKey(ColumnDataType.Model))
			{
				var modelCell = XlsRow.GetCell(ColumnsMap[ColumnDataType.Model]);
				string model = null;
				if (modelCell.CellType == CellType.String)
					model = modelCell.StringCellValue;
				else if(modelCell.CellType == CellType.Numeric)
					model = modelCell.NumericCellValue.ToString();

				Code = model;
				if (Diameter == null)
					return;

				if(!String.IsNullOrWhiteSpace(model))
				{
					var foundList = Repository.FittingRepository.GetFittings(wc.UoW, model, Diameter);
					if(foundList.Count == 1)
					{
						Status = ReadingXlsStatus.FoundModel;
						Fitting = foundList.First();
						return;
					}
					else if(foundList.Count > 1)
					{
						Status = ReadingXlsStatus.MultiFound;
						IsMultiFound = true;
						return;
					}
				}
				Status = ReadingXlsStatus.NotFound;
			}
		}

		public void UpdateCreatingStatus()
		{
			if (Fitting != null)
				return;

			if (Status == ReadingXlsStatus.BadDiameter && Diameter == null)
				return;

			if (Status == ReadingXlsStatus.MultiFound)
				return;

			if (Diameter != null && Pressure != null && Name != null && ConnectionType != null)
				Status = ReadingXlsStatus.WillCreated;
			else
				Status = ReadingXlsStatus.NotFound;
		}

		#region Поля для создания нового Fitting

		FittingType name;

		public virtual FittingType Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		Diameter diameter;
		public virtual Diameter Diameter {
			get { return diameter; }
			set { SetField (ref diameter, value, () => Diameter); }
		}

		DiameterUnits diameterUnits;

		public virtual DiameterUnits DiameterUnits {
			get { return diameterUnits; }
			set { SetField (ref diameterUnits, value, () => DiameterUnits); }
		}

		Pressure pressure;
		public virtual Pressure Pressure {
			get { return pressure; }
			set { SetField (ref pressure, value, () => Pressure); }
		}

		PressureUnits pressureUnits;

		public virtual PressureUnits PressureUnits {
			get { return pressureUnits; }
			set { SetField (ref pressureUnits, value, () => PressureUnits); }
		}

		ConnectionType connectionType;
		public virtual ConnectionType ConnectionType {
			get { return connectionType; }
			set { SetField (ref connectionType, value, () => ConnectionType); }
		}

		BodyMaterial bodyMaterial;
		public virtual BodyMaterial BodyMaterial {
			get { return bodyMaterial; }
			set { SetField (ref bodyMaterial, value, () => BodyMaterial); }
		}

		string code;
		public virtual string Code {
			get { return code; }
			set { SetField (ref code, value, () => Code); }
		}

		string note;

		public virtual string Note {
			get { return note; }
			set { SetField (ref note, value, () => Note); }
		}

		#endregion

		#region Расчетные

		public string DNText
		{
			get
			{
				return ColumnsMap.ContainsKey(ColumnDataType.DN) ? ToString(ColumnsMap[ColumnDataType.DN]) : null;
			}
		}
			
		public string PNText
		{
			get
			{
				return ColumnsMap.ContainsKey(ColumnDataType.PN) ? ToString(ColumnsMap[ColumnDataType.PN]) : null;
			}
		}

		public string ModelText
		{
			get
			{
				return ColumnsMap.ContainsKey(ColumnDataType.Model) ? ToString(ColumnsMap[ColumnDataType.Model]) : null;
			}
		}

		public string PriceText
		{
			get
			{
				return ColumnsMap.ContainsKey(ColumnDataType.Price) ? ToString(ColumnsMap[ColumnDataType.Price]) : null;
			}
		}

		#endregion

		#region Для отображения полей Fitting

		public virtual string DispalyPressure{ get { return Fitting != null 
					? GetPressureText(Fitting.Pressure, Fitting.PressureUnits) 
						: GetPressureText(Pressure, PressureUnits);
			} }

		private string GetPressureText(Pressure pressure, PressureUnits units)
		{
			if (pressure == null)
				return String.Empty;
			return units == PressureUnits.PN ? pressure.Pn : pressure.Pclass;
		}

		public virtual string DispalyDiameter{ get {
				return Fitting != null 
					? GetDiameterText(Fitting.Diameter, Fitting.DiameterUnits) 
						: GetDiameterText(Diameter, DiameterUnits);
			} }

		private string GetDiameterText(Diameter diameter, DiameterUnits units)
		{
			if (diameter == null)
					return String.Empty;
			return units == DiameterUnits.inch ? diameter.Inch : diameter.DN;
		}

		public virtual string DispalyType{ get {
				return Fitting != null ? Fitting.Name.NameEng : Name?.NameEng;
			} }

		public virtual string DispalyModel{ get {
				return Fitting != null ? Fitting.Code : Code;
			} }

		public virtual string DispalyConnection{ get {
				return Fitting != null ? Fitting.ConnectionType.NameEng : ConnectionType?.NameEng;
			} }

		public virtual string DispalyMaterial{ get {
				return Fitting != null ? Fitting.BodyMaterial?.NameEng : BodyMaterial?.NameEng;
			} }

		#endregion
	}

	public enum ColumnDataType{
		DN,
		PN,
		[Display(Name = "Модель")]
		Model,
		[Display(Name = "Цена")]
		Price
	}

	public enum ReadingXlsStatus{
		[Display(Name = "Не обработано")]
		None,
		[Display(Name = "Не определен DN")]
		BadDiameter,
		[Display(Name = "Найдено")]
		FoundModel,
		[Display(Name = "Установлено")]
		ManualSet,
		[Display(Name = "Найдено несколько")]
		MultiFound,
		[Display(Name = "Не найдено")]
		NotFound,
		[Display(Name = "Новая арматура")]
		WillCreated,
	}
}
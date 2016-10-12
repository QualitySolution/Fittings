using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QSOrmProject;
using Fittings.Domain;
using NPOI.SS.UserModel;

namespace Fittings
{
	public class UpdatingXLSRow : PropertyChangedBase
	{
		public NPOI.SS.UserModel.IRow XlsRow;

		public UpdatingXLSWorkClass WC;

		public Dictionary<ColumnType, int> ColumnsMap;

		public decimal? Price { get; set; }

		bool changePrice;
		public bool ChangePrice{
			get	{ return changePrice;}
			set	{ SetField (ref changePrice, value, () => ChangePrice);}
		}

		RowStatus status;
		public RowStatus Status{
			get	{ return status;}
			set	{ SetField (ref status, value, () => Status);}
		}

		public bool IsMultiFound { get; private set;}

		public List<Fitting> Fittings { get; set;}

		public List<PriceItem> Prices { get; set;}

		PriceItem selectedPrice;
		public PriceItem SelectedPrice
		{
			get{return selectedPrice;}
			set{
				SetField(ref selectedPrice, value, () => SelectedPrice);
				if (NewPrice == Price)
					Status = RowStatus.PriceNotChanged;
			}
		}

		public UpdatingXLSRow(NPOI.SS.UserModel.IRow row)
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

		public void TryParse()
		{
			//Парсим цену
			var priceCell = XlsRow.GetCell(ColumnsMap[ColumnType.Price]);
			if(priceCell == null)
			{
				Status = RowStatus.Skiped;
				return;
			}
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
			var dnCell = XlsRow.GetCell(ColumnsMap[ColumnType.DN]);
			if(dnCell == null)
			{
				Status = RowStatus.Skiped;
				return;
			}

			string dnAsString = null;
			if (dnCell.CellType == CellType.Numeric)
				dnAsString = dnCell.NumericCellValue.ToString();
			else if (dnCell.CellType == CellType.String)
				dnAsString = dnCell.StringCellValue;

			if(!String.IsNullOrWhiteSpace(dnAsString))
				WC.ParseDiameter(dnAsString, this);
			else
			{
				Status = RowStatus.Skiped;
				return;
			}

			if (Diameter == null)
			{
				Status = RowStatus.BadDiameter;
			}

			//Находим номенклатуру.
			if (ColumnsMap.ContainsKey(ColumnType.Model))
			{
				var modelCell = XlsRow.GetCell(ColumnsMap[ColumnType.Model]);
				string model = null;
				if (modelCell.CellType == CellType.String)
					model = modelCell.StringCellValue;
				else if (modelCell.CellType == CellType.Numeric)
					model = modelCell.NumericCellValue.ToString();

				Code = model;
				if (Diameter == null)
					return;

				if (!String.IsNullOrWhiteSpace(model))
				{
					var foundList = Repository.FittingRepository.GetFittings(WC.UoW, model, Diameter);
					if (foundList.Count > 0)
					{
						Status = RowStatus.OnlyModelFound;
						Fittings = foundList.ToList();

						Prices = Repository.PriceRepository.GetLastPrices(WC.UoW, Fittings.ToArray()).ToList();

						if(Prices.Count == 1)
						{
							Status = RowStatus.AutoNewPrice;
							SelectedPrice = Prices.First();
							ChangePrice = CanChangePrice;
						}
						else if (Prices.Count > 1)
						{
							Status = RowStatus.MultiFound;
							IsMultiFound = true;
						}
						return;
					}
				}
				Status = RowStatus.NotFound;
			}
			else
				Status = RowStatus.Skiped;
		}

		public void SetSelectedPrice(PriceItem item)
		{
			SelectedPrice = item;
		}
			
		#region Поля для создания нового Fitting

		Diameter diameter;
		public virtual Diameter Diameter {
			get { return diameter; }
			set { SetField (ref diameter, value, () => Diameter); }
		}

		Pressure pressure;
		public virtual Pressure Pressure {
			get { return pressure; }
			set { SetField (ref pressure, value, () => Pressure); }
		}

		string code;
		public virtual string Code {
			get { return code; }
			set { SetField (ref code, value, () => Code); }
		}

		#endregion

		#region Для отображения полей Fitting

		public bool CanChangePrice
		{
			get{
				bool priceDiff = NewPrice != Price;
				bool orthodoxStatus = (Status == RowStatus.ManualSet || Status == RowStatus.AutoNewPrice);
				return  priceDiff && orthodoxStatus;
			}
		}

		public decimal? NewPrice{
			get{
				if (SelectedPrice == null)
					return null;
				return Math.Round(QSCurrency.CBR.CurrencyConverter.Convert(SelectedPrice.Cost, SelectedPrice.Currency.ToString(), WC.Currency.ToString()).Value, 2);
			}
		}

		public string DisplayNewPrice{
			get{ 
				if (SelectedPrice != null)
					return String.Format("{0:N2} {1}",
						QSCurrency.CBR.CurrencyConverter.Convert(SelectedPrice.Cost, SelectedPrice.Currency.ToString(), WC.Currency.ToString()),
						WC.Currency
					);
				else if(Prices != null && Prices.Count > 0)
				{
					var list = Prices.Select(x => String.Format("{0:N2} {1}",
						QSCurrency.CBR.CurrencyConverter.Convert(x.Cost, x.Currency.ToString(), WC.Currency.ToString()),
						WC.Currency
					));
					return String.Join("\n", list);
				}
				return null;
			}
		}

		public string DisplayProvider{
			get{ 
				if (SelectedPrice != null)
					return SelectedPrice.Price.Provider.Name;
				else if(Prices != null && Prices.Count > 0)
				{
					var list = Prices.Select(x => x.Price.Provider.Name);
					return String.Join("\n", list);
				}
				return null;
			}
		}

		#endregion

		public enum ColumnType{
			DN,
			PN,
			[Display(Name = "Модель")]
			Model,
			[Display(Name = "Цена")]
			Price
		}

		public enum RowStatus{
			[Display(Name = "Не обработано")]
			None,
			[Display(Name = "Пропущено")]
			Skiped,
			[Display(Name = "Не определен DN")]
			BadDiameter,
			[Display(Name = "Номенклатура без цены")]
			OnlyModelFound,
			[Display(Name = "Выбрана цена")]
			ManualSet,
			[Display(Name = "Несколько поставщиков")]
			MultiFound,
			[Display(Name = "Не найдена арматура")]
			NotFound,
			[Display(Name = "Есть новая цена")]
			AutoNewPrice,
			[Display(Name = "Цена не изменилась")]
			PriceNotChanged,
		}
	}
}
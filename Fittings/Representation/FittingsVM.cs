using System;
using System.Collections.Generic;
using Gamma.ColumnConfig;
using NHibernate.Transform;
using QSOrmProject;
using QSOrmProject.RepresentationModel;
using Fittings.Domain;
using Fittings;
using NHibernate.Criterion;

namespace Fittings.ViewModel
{
	public class FittingsVM : RepresentationModelEntityBase<Fitting, FittingVMNode>
	{
		public FittingsFlt Filter {
			get {
				return RepresentationFilter as FittingsFlt;
			}
			set {
				RepresentationFilter = value as IRepresentationFilter;
			}
		}

		#region IRepresentationModel implementation

		public override void UpdateNodes ()
		{
			Fitting fittingAlias = null;
			FittingType typeAlias = null;
			FittingVMNode resultAlias = null;
			Diameter diameterAlias = null;
			Pressure pressureAlias = null;
			ConnectionType connectionTypeAlias = null;
			BodyMaterial bodyMaterialAlias = null;

			PriceItem pricePriceItemAlias = null;
			PriceItem datePriceItemAlias = null;
			PriceItem currencyPriceItemAlias = null;
			Price pricePriceAlias = null;
			Price datePriceAlias = null;
			Price currencyPriceAlias = null;


			var priceSubQuery = QueryOver.Of<PriceItem> (() => pricePriceItemAlias)
				.JoinAlias (c => c.Price, () => pricePriceAlias)
				.Where (() => pricePriceItemAlias.Fitting.Id == fittingAlias.Id)
				.Select(x => x.Cost)
				.OrderBy(() => pricePriceAlias.Date).Desc()
				.Take(1);

			var currencySubQuery = QueryOver.Of<PriceItem> (() => currencyPriceItemAlias)
				.JoinAlias (c => c.Price, () => currencyPriceAlias)
				.Where (() => currencyPriceItemAlias.Fitting.Id == fittingAlias.Id)
				.Select(x => x.Currency)
				.OrderBy(() => currencyPriceAlias.Date).Desc
				.Take(1);
			
			var dateSubQuery = QueryOver.Of<PriceItem> (() => datePriceItemAlias)
				.JoinAlias (c => c.Price, () => datePriceAlias)
				.Where (() => datePriceItemAlias.Fitting.Id == fittingAlias.Id)
				.SelectList(list => list.SelectMax(() => datePriceAlias.Date)).Take(1);

			var fittingQuery = UoW.Session.QueryOver<Fitting> (() => fittingAlias)
				.JoinAlias (c => c.Name, () => typeAlias)
				.JoinAlias (c => c.Diameter, () => diameterAlias)
				.JoinAlias (c => c.Pressure, () => pressureAlias)
				.JoinAlias (c => c.ConnectionType, () => connectionTypeAlias)
				.JoinAlias (c => c.BodyMaterial, () => bodyMaterialAlias);

			if (Filter.RestrictFittingType != null)
				fittingQuery.Where (() => fittingAlias.Name.Id == Filter.RestrictFittingType.Id);

			if (Filter.RestrictBodyMaterial != null)
				fittingQuery.Where (() => fittingAlias.BodyMaterial.Id == Filter.RestrictBodyMaterial.Id);

			if (Filter.RestrictConnectionType != null)
				fittingQuery.Where (() => fittingAlias.ConnectionType.Id == Filter.RestrictConnectionType.Id);

			if (Filter.RestrictDiameter != null)
				fittingQuery.Where (() => fittingAlias.Diameter.Id == Filter.RestrictDiameter.Id);

			if (Filter.RestrictPressure != null)
				fittingQuery.Where (() => fittingAlias.Pressure.Id == Filter.RestrictPressure.Id);
			
			var fittinglist =	fittingQuery.SelectList(list => list
					.Select(() => fittingAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => typeAlias.NameRus).WithAlias(() => resultAlias.Name)

					.Select(() => diameterAlias.Inch).WithAlias(() => resultAlias.DiameterInch)
					.Select(() => diameterAlias.Mm).WithAlias(() => resultAlias.DiameterMm)
					.Select(() => fittingAlias.DiameterUnits).WithAlias(() => resultAlias.DiameterUnits)

					.Select(() => pressureAlias.Pn).WithAlias(() => resultAlias.PressurePn)
					.Select(() => pressureAlias.Pclass).WithAlias(() => resultAlias.PressurePclass)
					.Select(() => fittingAlias.PressureUnits).WithAlias(() => resultAlias.PressureUnits)

					.Select(() => connectionTypeAlias.NameRus).WithAlias(() => resultAlias.ConnectionType)
					.Select(() => bodyMaterialAlias.NameRus).WithAlias(() => resultAlias.BodyMaterial)
					.Select(() => fittingAlias.Code).WithAlias(() => resultAlias.Code)
					.Select(() => fittingAlias.Note).WithAlias(() => resultAlias.Note)

					.SelectSubQuery(priceSubQuery).WithAlias(() => resultAlias.Price)
					.SelectSubQuery(currencySubQuery).WithAlias(() => resultAlias.PriceСurrency)
					.SelectSubQuery(dateSubQuery).WithAlias(() => resultAlias.PriceDate)
				)
				.TransformUsing(Transformers.AliasToBean<FittingVMNode>())
				.List<FittingVMNode>();

			SetItemsSource (fittinglist);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig <FittingVMNode>.Create ()
			.AddColumn ("Тип").SetDataProperty (node => node.Name)
			.AddColumn ("Диаметр").SetDataProperty (node => node.Diameter)
			.AddColumn ("Давление").SetDataProperty (node => node.Pressure)
			.AddColumn ("Тип соединения").SetDataProperty (node => node.ConnectionType)
			.AddColumn ("Материал корпуса").SetDataProperty (node => node.BodyMaterial)
			.AddColumn ("Артикул").SetDataProperty (node => node.Code)
			.AddColumn ("Стоимость").SetDataProperty(node => node.PriceText)
			.AddColumn ("Прайс").AddTextRenderer(node => node.PriceDate.HasValue ? node.PriceDate.Value.ToString("d") : String.Empty)
			.AddColumn ("Комментарий").SetDataProperty (node => node.Note)
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#endregion

		protected override bool NeedUpdateFunc (Fitting updatedSubject)
		{
			return true;
		}

		public FittingsVM (FittingsFlt filter) : this (filter.UoW)
		{
			Filter = filter;
		}

		public FittingsVM ()
			: this (UnitOfWorkFactory.CreateWithoutRoot ())
		{
			CreateRepresentationFilter = () => new FittingsFlt (UoW);
		}

		public FittingsVM (IUnitOfWork uow)
		{
			this.UoW = uow;
		}
	}

	public class FittingVMNode
	{
		public int Id{ get; set;}

		public string Name{ get; set;}

		public string DiameterMm{ get; set;}
		public string DiameterInch{ get; set;}
		public DiameterUnits DiameterUnits{ get; set;}
		public string Diameter{ get{return DiameterUnits == DiameterUnits.inch ? DiameterInch : DiameterMm;}}

		public string PressurePn{ get; set;}
		public string PressurePclass{ get; set;}
		public PressureUnits PressureUnits{ get; set;}
		public string Pressure{ get{return PressureUnits == PressureUnits.PN ? PressurePn : PressurePclass;}}

		public string ConnectionType{ get; set;}

		public string BodyMaterial{ get; set;}

		public decimal? Price{ get; set;}

		public PriceСurrency? PriceСurrency{ get; set;}

		public DateTime? PriceDate{ get; set;}

		[UseForSearch]
		[SearchHighlight]
		public string Code{ get; set;}

		[UseForSearch]
		[SearchHighlight]
		public string Note{ get; set;}

		public string PriceText { get { 
				if (Price.HasValue && PriceСurrency.HasValue)
					return string.Format ("{0} {1}", Price.Value, PriceСurrency.Value);
				else
					return String.Empty;
		}}
	}
}



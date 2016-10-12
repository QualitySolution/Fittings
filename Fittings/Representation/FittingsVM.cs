using System;
using System.Collections.Generic;
using Fittings;
using Fittings.Domain;
using Gamma.ColumnConfig;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.Transform;
using QSOrmProject;
using QSOrmProject.RepresentationModel;

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
			Price pricePriceAlias = null;
			Price datePriceAlias = null;
			Provider providerAlias = null;

			var dateSubQuery = QueryOver.Of<PriceItem> (() => datePriceItemAlias)
				.JoinAlias (c => c.Price, () => datePriceAlias)
				.Where (() => datePriceItemAlias.Fitting.Id == fittingAlias.Id)
				.Where(() => datePriceAlias.Provider.Id == pricePriceAlias.Provider.Id)
				.Select(Projections.SqlFunction(
					new SQLFunctionTemplate(NHibernateUtil.String, "SUBSTRING_INDEX(?1, ?2, ?3)"),
					NHibernateUtil.Int32,
					Projections.SqlFunction(
						new SQLFunctionTemplate(NHibernateUtil.String, "GROUP_CONCAT( ?1 ORDER BY ?2)"),
						NHibernateUtil.String,
						Projections.Property(() => datePriceAlias.Id),
						Projections.Property(() => datePriceAlias.Date)),
					Projections.Constant (","),
					Projections.Constant (-1)
				));

			var priceSubQuery = QueryOver.Of<PriceItem> (() => pricePriceItemAlias)
				.JoinAlias (c => c.Price, () => pricePriceAlias)
				.JoinAlias( () => pricePriceAlias.Provider, () => providerAlias)
				.Where (() => pricePriceItemAlias.Fitting.Id == fittingAlias.Id)
				.WithSubquery.WhereProperty(() => pricePriceAlias.Id).In(dateSubQuery)
				.Select(Projections.SqlFunction (
						new SQLFunctionTemplate (NHibernateUtil.String, "GROUP_CONCAT( ?1 SEPARATOR ?2)"),
						NHibernateUtil.String,
						Projections.SqlFunction("CONCAT", NHibernateUtil.String,
							Projections.Property (() => providerAlias.Name),
							Projections.Constant (" - "),
							Projections.Property (() => pricePriceItemAlias.Cost),
							Projections.Constant (" "),
							Projections.Property (() => pricePriceItemAlias.Currency),
							Projections.Constant (" ("),
							Projections.SqlFunction(
							new SQLFunctionTemplate (NHibernateUtil.String, "DATE_FORMAT( ?1, ?2)"),
							NHibernateUtil.String,
							Projections.Property (() => pricePriceAlias.Date),
							Projections.Constant ("%d.%m.%Y")
						),
							Projections.Constant (")")
						),
						Projections.Constant ("\n")))
				.Take(1);

			var fittingQuery = UoW.Session.QueryOver<Fitting> (() => fittingAlias)
				.JoinAlias (c => c.Name, () => typeAlias)
				.JoinAlias (c => c.Diameter, () => diameterAlias)
				.JoinAlias (c => c.Pressure, () => pressureAlias)
				.JoinAlias (c => c.ConnectionType, () => connectionTypeAlias)
				.JoinAlias (c => c.BodyMaterial, () => bodyMaterialAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

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

			if (!String.IsNullOrEmpty(Filter.RestrictModel))
				fittingQuery.Where(() => fittingAlias.Code == Filter.RestrictModel);
			
			var fittinglist =	fittingQuery.SelectList(list => list
					.Select(() => fittingAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => typeAlias.NameRus).WithAlias(() => resultAlias.Name)

					.Select(() => diameterAlias.Inch).WithAlias(() => resultAlias.DiameterInch)
					.Select(() => diameterAlias.DN).WithAlias(() => resultAlias.DiameterMm)
					.Select(() => fittingAlias.DiameterUnits).WithAlias(() => resultAlias.DiameterUnits)

					.Select(() => pressureAlias.Pn).WithAlias(() => resultAlias.PressurePn)
					.Select(() => pressureAlias.Pclass).WithAlias(() => resultAlias.PressurePclass)
					.Select(() => fittingAlias.PressureUnits).WithAlias(() => resultAlias.PressureUnits)

					.Select(() => connectionTypeAlias.NameRus).WithAlias(() => resultAlias.ConnectionType)
					.Select(() => bodyMaterialAlias.NameRus).WithAlias(() => resultAlias.BodyMaterial)
					.Select(() => fittingAlias.Code).WithAlias(() => resultAlias.Code)
					.Select(() => fittingAlias.Note).WithAlias(() => resultAlias.Note)

					.SelectSubQuery(priceSubQuery).WithAlias(() => resultAlias.ProvidersPrice)
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
			.AddColumn ("Стоимость").SetDataProperty(node => node.ProvidersPrice)
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

		public string ProvidersPrice{ get; set;}

		[UseForSearch]
		[SearchHighlight]
		public string Code{ get; set;}

		[UseForSearch]
		[SearchHighlight]
		public string Note{ get; set;}
	}
}



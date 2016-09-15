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
	public class FittingPricesVM : RepresentationModelEntitySubscribingBase<PriceItem, FittingPricesVMNode>
	{
		public Fitting Fitting { get; set;}

		#region IRepresentationModel implementation

		public override void UpdateNodes ()
		{
			FittingPricesVMNode resultAlias = null;

			PriceItem priceItemAlias = null;
			Price priceAlias = null;
			Provider providerAlias = null;

			var pricesQuery = UoW.Session.QueryOver<PriceItem> (() => priceItemAlias)
				.JoinAlias (c => c.Price, () => priceAlias)
				.JoinAlias(() => priceAlias.Provider, () => providerAlias)
				.Where (() => priceItemAlias.Fitting.Id == Fitting.Id);

			var pricesList =	pricesQuery.SelectList(list => list
				.Select(() => priceItemAlias.Id).WithAlias(() => resultAlias.Id)
				.Select(() => providerAlias.Name).WithAlias(() => resultAlias.Provider)
				.Select(() => priceAlias.Date).WithAlias(() => resultAlias.PriceDate)
				.Select(() => priceItemAlias.Cost).WithAlias(() => resultAlias.Price)
				.Select(() => priceItemAlias.Currency).WithAlias(() => resultAlias.PriceСurrency)
				)
				.OrderBy(() => priceAlias.Date).Desc()
				.TransformUsing(Transformers.AliasToBean<FittingPricesVMNode>())
				.List<FittingPricesVMNode>();

			SetItemsSource (pricesList);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig <FittingPricesVMNode>.Create ()
			.AddColumn ("Прайс от").AddTextRenderer (node => node.PriceDate.ToShortDateString())
			.AddColumn ("Поставщик").SetDataProperty (node => node.Provider)
			.AddColumn ("Цена").SetDataProperty (node => node.PriceText)
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#region implemented abstract members of RepresentationModelEntityBase

		protected override bool NeedUpdateFunc(PriceItem updatedSubject)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region implemented abstract members of RepresentationModelEntitySubscribingBase

		protected override bool NeedUpdateFunc(object updatedSubject)
		{
			if (updatedSubject is PriceItem)
				return (updatedSubject as PriceItem).Fitting.Id == Fitting.Id;
			return true;
		}

		#endregion

		#endregion
			
		public FittingPricesVM (Fitting fitting)
			: this (UnitOfWorkFactory.CreateWithoutRoot ())
		{
			Fitting = fitting;
		}

		public FittingPricesVM (IUnitOfWork uow) : base(
			typeof(PriceItem),
			typeof(Price)
		)
		{
			this.UoW = uow;
		}
	}

	public class FittingPricesVMNode
	{
		public int Id{ get; set;}

		public DateTime PriceDate{ get; set;}

		public string Provider{ get; set;}

		public decimal Price{ get; set;}

		public PriceСurrency PriceСurrency{ get; set;}

		public string PriceText { get { 
				return string.Format ("{0} {1}", Price, PriceСurrency);
		}}
	}
}



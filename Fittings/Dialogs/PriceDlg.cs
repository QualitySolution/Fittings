using System;
using QSOrmProject;
using Fittings.Domain;
using System.Linq;
using Gamma.GtkWidgets;
using Fittings.ViewModel;

namespace Fittings
{
	public partial class PriceDlg : OrmGtkDialogBase<Price>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();
		PriceItem editingItem;

		public PriceDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Price> ();
			ConfigureDlg ();
		}

		public PriceDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Price> (id);
			ConfigureDlg ();
		}

		public PriceDlg (Price sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			providerReference.SubjectType = typeof(Provider);
			providerReference.Binding.AddBinding (Entity, e => e.Provider, w => w. Subject).InitializeFromSource(); 
			commentTextview.Binding.AddBinding (Entity, e => e.Comment, w => w.Buffer.Text).InitializeFromSource();
			datepicker.Binding.AddBinding (Entity, e => e.Date, w => w.Date).InitializeFromSource();
			pricesTreeView.ColumnsConfig = ColumnsConfigFactory.Create <PriceItem> ()
				.AddColumn ("Арматура")
					.AddTextRenderer (x => x.Fitting.Name.NameRus)
				.AddColumn ("Диаметр")
					.AddTextRenderer (x => x.Fitting.DiameterText)
				.AddColumn ("Давление")
					.AddTextRenderer (x => x.Fitting.PressureText)
				.AddColumn ("Цена")
				.AddEnumRenderer (x => x.Currency).Editing()
				.AddNumericRenderer (x => x.Cost).Editing (new Gtk.Adjustment (0, 0, 10000000, 1, 100, 100)).Digits (2)
				.Finish();

			pricesTreeView.Selection.Changed += PricesTreeView_Selection_Changed;
			pricesTreeView.ItemsDataSource = Entity.ObservablePrices;
		}

		void PricesTreeView_Selection_Changed (object sender, EventArgs e)
		{
			buttonEdit1.Sensitive = buttonRemove.Sensitive = pricesTreeView.Selection.CountSelectedRows() > 0 ;
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Price> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем информацию о проекте...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;
		}

		protected void OnButtonAddClicked (object sender, EventArgs e)
		{
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.MultiSelect;
			dlg.ObjectSelected += Dlg_ObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_ObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var fittings = UoW.GetById<Fitting> (e.GetNodes<FittingVMNode> ().Select (x => x.Id).ToArray());

			PriceСurrency defaulCurr = PriceСurrency.USD;
			if(Entity.Prices.Count > 0)
				defaulCurr = Entity.Prices.GroupBy (x => x.Currency)
				.Select (g => new { 
						Currency = g.Key, 
						Count = g.Count (), 
				}).OrderByDescending (x => x.Count).First ().Currency;

			foreach (var item in e.GetNodes<FittingVMNode>()) {
				Entity.AddItem( fittings.First(x => x.Id == item.Id), defaulCurr);
			}
		}

		void Dlg_EditObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var fitting = UoW.GetById<Fitting> (e.ObjectId);
			editingItem.Fitting = fitting;
		}

		protected void OnButtonEdit1Clicked (object sender, EventArgs e)
		{
			editingItem = pricesTreeView.GetSelectedObject<PriceItem> ();
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += Dlg_EditObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		protected void OnButtonRemoveClicked (object sender, EventArgs e)
		{
			Entity.ObservablePrices.Remove (pricesTreeView.GetSelectedObject<PriceItem> ());
		}
	}
}


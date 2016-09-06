using System;
using QSOrmProject;
using Fittings.Domain;
using System.Linq;
using Gamma.GtkWidgets;

namespace Fittings
{
	public partial class PriceDlg : OrmGtkDialogBase<Price>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

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

			pricesTreeView.ItemsDataSource = Entity.ObservablePrices;
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
	}
}


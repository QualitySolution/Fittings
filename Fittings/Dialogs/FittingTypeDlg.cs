using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class FittingTypeDlg : OrmGtkDialogBase<FittingType>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public FittingTypeDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<FittingType> ();
			ConfigureDlg ();
		}

		public FittingTypeDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<FittingType> (id);
			ConfigureDlg ();
		}

		public FittingTypeDlg (FittingType sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			rusNameEntry.Binding.AddBinding (Entity, e => e.NameRus, w => w.Text).InitializeFromSource(); 
			engNameEntry.Binding.AddBinding (Entity, e => e.NameEng, w => w.Text).InitializeFromSource();
			yentryModelCode.Binding.AddBinding (Entity, e => e.ModelCode, w => w.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<FittingType> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем тип соединения...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


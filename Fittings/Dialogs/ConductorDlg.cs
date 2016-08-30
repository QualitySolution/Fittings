using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class ConductorDlg : OrmGtkDialogBase<Conductor>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public ConductorDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Conductor> ();
			ConfigureDlg ();
		}

		public ConductorDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Conductor> (id);
			ConfigureDlg ();
		}

		public ConductorDlg (Conductor sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			rusNameEntry.Binding.AddBinding (Entity, e => e.NameRus, w => w.Text).InitializeFromSource(); 
			engNameEntry.Binding.AddBinding (Entity, e => e.NameEng, w => w.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Conductor> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем вид проводимой среды...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


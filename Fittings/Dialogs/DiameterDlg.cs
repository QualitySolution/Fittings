using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class DiameterDlg : OrmGtkDialogBase<Diameter>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public DiameterDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Diameter> ();
			ConfigureDlg ();
		}

		public DiameterDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Diameter> (id);
			ConfigureDlg ();
		}

		public DiameterDlg (Diameter sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			inchEntry.Binding.AddBinding (Entity, e => e.Inch, w => w.Text).InitializeFromSource(); 
			mmEntry.Binding.AddBinding (Entity, e => e.Mm, w => w.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Diameter> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем давление...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


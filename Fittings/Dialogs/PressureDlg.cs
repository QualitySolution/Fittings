using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class PressureDlg : OrmGtkDialogBase<Pressure>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public PressureDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Pressure> ();
			ConfigureDlg ();
		}

		public PressureDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Pressure> (id);
			ConfigureDlg ();
		}

		public PressureDlg (Pressure sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			var converterToNull = new NullToEmptyStringConverter();
			pnEntry.Binding.AddBinding (Entity, e => e.Pn, w => w.Text, converterToNull).InitializeFromSource(); 
			pclassEntry.Binding.AddBinding (Entity, e => e.Pclass, w => w.Text, converterToNull).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Pressure> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем давление...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


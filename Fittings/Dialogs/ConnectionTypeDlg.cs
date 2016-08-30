using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class ConnectionTypeDlg : OrmGtkDialogBase<ConnectionType>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public ConnectionTypeDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<ConnectionType> ();
			ConfigureDlg ();
		}

		public ConnectionTypeDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<ConnectionType> (id);
			ConfigureDlg ();
		}

		public ConnectionTypeDlg (ConnectionType sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			rusNameEntry.Binding.AddBinding (Entity, e => e.NameRus, w => w.Text).InitializeFromSource(); 
			engNameEntry.Binding.AddBinding (Entity, e => e.NameEng, w => w.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<ConnectionType> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем тип соединения...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


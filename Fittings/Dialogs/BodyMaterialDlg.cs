using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class BodyMaterialDlg : OrmGtkDialogBase<BodyMaterial>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public BodyMaterialDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<BodyMaterial> ();
			ConfigureDlg ();
		}

		public BodyMaterialDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<BodyMaterial> (id);
			ConfigureDlg ();
		}

		public BodyMaterialDlg (BodyMaterial sub) : this (sub.Id) {}
			
		private void ConfigureDlg ()
		{
			rusNameEntry.Binding.AddBinding (Entity, e => e.NameRus, w => w.Text).InitializeFromSource(); 
			engNameEntry.Binding.AddBinding (Entity, e => e.NameEng, w => w.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<BodyMaterial> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем материал...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


using System;
using QSOrmProject;
using Fittings.Domain;

namespace Fittings
{
	public partial class ProjectDlg : OrmGtkDialogBase<Project>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public ProjectDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Project> ();
			ConfigureDlg ();
		}

		public ProjectDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Project> (id);
			ConfigureDlg ();
		}

		public ProjectDlg (Project sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			customerEntry.Binding.AddBinding (Entity, e => e.Customer, w => w.Text).InitializeFromSource(); 
			projectNameEntry.Binding.AddBinding (Entity, e => e.ProjectName, w => w.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Project> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем информацию о проекте...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}
	}
}


using System;
using QSOrmProject;
using Fittings.Domain;
using Gamma.GtkWidgets;

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
			projectTreeView.ColumnsConfig = ColumnsConfigFactory.Create <ProjectItem> ()
				.AddColumn ("Номер").AddTextRenderer (x => x.SequenceNumber.ToString())
				.AddColumn ("Позиция ТРП").AddTextRenderer (x => x.TrpPositions)
				.AddColumn ("Тип").AddTextRenderer (x => x.Name.NameRus)
				.AddColumn ("Кол-во").AddTextRenderer (x => x.Amount.ToString())
				.AddColumn ("Диаметр").AddTextRenderer (x => x.Fitting.DiameterText) 
				.AddColumn ("Давление").AddTextRenderer (x => x.Fitting.PressureText) 
				.AddColumn ("Тип соединения").AddTextRenderer (x => x.ConnectionType.NameRus)
				.AddColumn ("Проводимая среда").AddTextRenderer (x => x.Conductor.NameRus)
				.AddColumn ("Группа").AddTextRenderer (x => x.Group)
				.AddColumn ("Расположение").AddTextRenderer (x => x.Location)
				.AddColumn ("Min t").AddTextRenderer (x => x.TemperatureMin.ToString()).Editable()
				.AddColumn ("Max t").AddTextRenderer (x => x.TemperatureMax.ToString()).Editable()
				.AddColumn ("Комментарий").AddTextRenderer (x => x.Comment).Editable()
				.Finish();

			projectTreeView.ItemsDataSource = Entity.ObservableProjects;
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


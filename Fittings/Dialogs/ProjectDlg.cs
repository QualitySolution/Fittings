using System;
using QSOrmProject;
using Fittings.Domain;
using Gamma.GtkWidgets;
using Fittings.ViewModel;
using System.Linq;

namespace Fittings
{
	public partial class ProjectDlg : OrmGtkDialogBase<Project>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();
		ProjectItem editingItem;

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
				.AddColumn ("Позиция ТРП").AddTextRenderer (x => x.TrpPositions).Editable()
				.AddColumn ("Тип").AddTextRenderer (x => x.Name.NameRus)
				.AddColumn ("Кол-во").AddNumericRenderer (x => x.Amount).Editing (new Gtk.Adjustment(0, 0, 10000000, 1, 100, 100))
				.AddColumn ("Диаметр").AddTextRenderer (x => x.Fitting.DiameterText) 
				.AddColumn ("Давление").AddTextRenderer (x => x.Fitting.PressureText) 
				.AddColumn ("Тип соединения").AddTextRenderer (x => x.ConnectionType.NameRus)
				.AddColumn ("Проводимая среда").AddTextRenderer (x => x.Conductor.NameRus)
				.AddColumn ("Группа").AddTextRenderer (x => x.PrGroup).Editable()
				.AddColumn ("Расположение").AddTextRenderer (x => x.Location).Editable()
				.AddColumn ("Температура")
					.AddNumericRenderer (x => x.TemperatureMin).Editing (new Gtk.Adjustment(0, 0, 10000000, 1, 100, 100))
					.AddTextRenderer (x =>(" - "))
					.AddNumericRenderer (x => x.TemperatureMax).Editing (new Gtk.Adjustment(0, 0, 10000000, 1, 100, 100))
				.AddColumn ("Комментарий").AddTextRenderer (x => x.Comment).Editable()
				.Finish();
				
			projectTreeView.Selection.Changed += ProjectTreeView_Selection_Changed;
			projectTreeView.ItemsDataSource = Entity.ObservableProjectRows;
		}

		void ProjectTreeView_Selection_Changed (object sender, EventArgs e)
		{
			buttonEdit1.Sensitive = buttonRemove.Sensitive = projectTreeView.Selection.CountSelectedRows() > 0 ;
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

		protected void OnButtonAddClicked (object sender, EventArgs e)
		{
			var dlg = new OrmReference(typeof(FittingType));
			dlg.Mode = OrmReferenceMode.MultiSelect;
			dlg.ObjectSelected += Dlg_ObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		protected void OnButtonEdit1Clicked (object sender, EventArgs e)
		{
			editingItem = projectTreeView.GetSelectedObject<ProjectItem> ();
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += Dlg_EditObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_ObjectSelected (object sender, OrmReferenceObjectSectedEventArgs e)
		{
			Entity.AddItem (e.Subject as FittingType);
		}

		void Dlg_EditObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var fitting = UoW.GetById<Fitting> (e.ObjectId);
			editingItem.Fitting = fitting;
		}

		protected void OnButtonRemoveClicked (object sender, EventArgs e)
		{
			Entity.ObservableProjectRows.Remove (projectTreeView.GetSelectedObject<ProjectItem> ());
		}
	}
}


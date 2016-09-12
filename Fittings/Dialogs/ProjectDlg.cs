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

			var ConductorItemsList = UoW.GetAll<Conductor> ().ToList();

			projectTreeView.ColumnsConfig = ColumnsConfigFactory.Create <ProjectItem> ()
				.AddColumn ("Номер").AddTextRenderer (x => x.SequenceNumber.ToString()).Editable()
				.AddColumn ("Позиция ТРП").AddTextRenderer (x => x.TrpPositions).Editable()
				.AddColumn ("Тип").AddTextRenderer (x => x.Fitting.Name.NameRus)
				.AddColumn ("Кол-во").AddNumericRenderer (x => x.Amount).Editing (new Gtk.Adjustment(0, 0, 10000000, 1, 100, 100))
				.AddColumn ("Диаметр").AddTextRenderer (x => x.Fitting.DiameterText) 
				.AddColumn ("Давление").AddTextRenderer (x => x.Fitting.PressureText) 
				.AddColumn ("Тип соединения").AddTextRenderer (x => x.Fitting.ConnectionType.NameRus)
				.AddColumn ("Проводимая среда").AddComboRenderer (x => x.Conductor)
				.SetDisplayFunc(x => (x as Conductor).NameRus).FillItems<Conductor>(ConductorItemsList).Editing()
				.AddColumn ("Группа").AddTextRenderer (x => x.PrGroup).Editable() 
				.AddColumn ("Расположение").AddTextRenderer (x => x.Location).Editable()
				.AddColumn ("Температура")
				.AddNumericRenderer (x => x.TemperatureMin).Editing (new Gtk.Adjustment(0, -273, 2000, 1, 100, 100)).WidthChars(5)
					.AddTextRenderer (x =>("—"))
				.AddNumericRenderer (x => x.TemperatureMax).Editing (new Gtk.Adjustment(0, -273, 2000, 1, 100, 100)).WidthChars(5)
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
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.MultiSelect;
			dlg.ObjectSelected += Dlg_ObjectSelected1;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_ObjectSelected1 (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			var fittings = UoW.GetById<Fitting> (e.GetNodes<FittingVMNode> ().Select (x => x.Id).ToArray());
			foreach (var item in e.GetNodes<FittingVMNode>()) {
				Entity.AddItem (fittings.First (x => x.Id == item.Id));
			}
		}

		protected void OnButtonEdit1Clicked (object sender, EventArgs e)
		{
			editingItem = projectTreeView.GetSelectedObject<ProjectItem> ();
			var dlg = new ReferenceRepresentation (new FittingsVM ());
			dlg.Mode = OrmReferenceMode.Select;
			dlg.ObjectSelected += Dlg_EditObjectSelected;
			TabParent.AddSlaveTab(this, dlg);
		}

		void Dlg_EditObjectSelected (object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			editingItem.Fitting = UoW.GetById<Fitting> (e.ObjectId);
		}

		protected void OnButtonRemoveClicked (object sender, EventArgs e)
		{
			Entity.ObservableProjectRows.Remove (projectTreeView.GetSelectedObject<ProjectItem> ());
		}
	}
}


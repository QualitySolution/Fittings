using System;
using QSOrmProject;
using Fittings.Domain;
using System.Linq;

namespace Fittings
{
	public partial class FittingDlg : OrmGtkDialogBase<Fitting>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public FittingDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Fitting> ();
			ConfigureDlg ();
		}

		public FittingDlg (int id)
		{
			this.Build ();
			logger.Info ("Загрузка информации...");
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Fitting> (id);
			ConfigureDlg ();
		}

		public FittingDlg (Fitting sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			fittingTypeReference.SubjectType = typeof(FittingType);
			fittingTypeReference.Binding.AddBinding (Entity, e => e.Name, w => w. Subject).InitializeFromSource();

			diameterUnitscombobox.ItemsEnum = typeof(DiameterUnits);
			diameterUnitscombobox.Binding.AddBinding (Entity, e => e.DiameterUnits, w => w.SelectedItem).InitializeFromSource();
			diameterCombobox.ItemsList = UoW.GetAll<Diameter> ().ToList();
			diameterCombobox.Binding.AddBinding (Entity, e => e.Diameter, w => w.SelectedItem).InitializeFromSource(); 

			pressureUnitscombobox.ItemsEnum = typeof(PressureUnits);
			pressureUnitscombobox.Binding.AddBinding (Entity, e => e.PressureUnits, w => w.SelectedItem).InitializeFromSource();
			pressureCombobox.ItemsList = UoW.GetAll<Pressure> ().ToList();
			pressureCombobox.Binding.AddBinding (Entity, e => e.Pressure, w => w.SelectedItem).InitializeFromSource(); 

			connectionTypeRreference.SubjectType = typeof(ConnectionType);
			connectionTypeRreference.Binding.AddBinding (Entity, e => e.ConnectionType, w => w.Subject).InitializeFromSource();

			bodyMaterialReference.SubjectType = typeof(BodyMaterial);
			bodyMaterialReference.Binding.AddBinding (Entity, e => e.BodyMaterial, w => w.Subject).InitializeFromSource();

			codeEntry.Binding.AddBinding (Entity, e => e.Code, w => w.Text).InitializeFromSource();
			commentTextview.Binding.AddBinding (Entity, e => e.Note, w => w.Buffer.Text).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidation.QSValidator<Fitting> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;
			logger.Info ("Сохраняем арматуру...");
			UoWGeneric.Save ();
			logger.Info ("Ok");
			return true;

		}

		protected void OnDiameterUnitscomboboxChanged (object sender, EventArgs e)
		{
			switch (Entity.DiameterUnits) {
			case DiameterUnits.inch:
				diameterCombobox.SetRenderTextFunc<Diameter> (x => x.Inch);
				break;
			case DiameterUnits.mm:
				diameterCombobox.SetRenderTextFunc<Diameter> (x => x.Mm.ToString());
				break;
			}
		}

		protected void OnPressureUnitscomboboxChanged (object sender, EventArgs e)
		{
			switch (Entity.PressureUnits) {
			case PressureUnits.PN:
				pressureCombobox.SetRenderTextFunc<Pressure> (x => x.Pn.ToString());
				break;
			case PressureUnits.Pclass:
				pressureCombobox.SetRenderTextFunc<Pressure> (x => x.Pclass.ToString());
				break;
			}
		}


	}
}


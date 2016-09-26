using System;
using QSOrmProject.RepresentationModel;
using QSOrmProject;
using Fittings.Domain;
using System.Linq;

namespace Fittings
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class FittingsFlt : Gtk.Bin, IRepresentationFilter
	{
		IUnitOfWork uow;

		public IUnitOfWork UoW {
			get {
				return uow;
			}
			set {
				uow = value;
			}
		}

		public FittingsFlt (IUnitOfWork uow)
		{
			this.Build ();
			UoW = uow;
			ConfigureDlg ();
		}

		private void ConfigureDlg ()
		{
			fittingTypeReference.SubjectType = typeof(FittingType);
			connectionTypeRreference.SubjectType = typeof(ConnectionType);
			bodyMaterialReference.SubjectType = typeof(BodyMaterial);
			pressureUnitscombobox.ItemsEnum = typeof(PressureUnits);
			pressureCombobox.ItemsList = UoW.GetAll<Pressure> ().ToList();
			diameterCombobox.ItemsList = UoW.GetAll<Diameter> ().ToList();
			diameterUnitscombobox.ItemsEnum = typeof(DiameterUnits);
		}

		public event EventHandler Refiltered;

		void OnRefiltered ()
		{
			if (Refiltered != null)
				Refiltered (this, new EventArgs ());
		}

		protected void OnDiameterUnitscomboboxChanged (object sender, EventArgs e)
		{
			switch ((DiameterUnits)diameterUnitscombobox.SelectedItem) {
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
			switch ((PressureUnits)pressureUnitscombobox.SelectedItem) {
			case PressureUnits.PN:
				pressureCombobox.SetRenderTextFunc<Pressure> (x => x.Pn.ToString());
				break;
			case PressureUnits.Pclass:
				pressureCombobox.SetRenderTextFunc<Pressure> (x => x.Pclass.ToString());
				break;
			}
		}

		public FittingType RestrictFittingType {
			get { return fittingTypeReference.Subject as FittingType; }
			set {
				fittingTypeReference.Subject = value;
				fittingTypeReference.Sensitive = false;
			}
		}

		public BodyMaterial RestrictBodyMaterial {
			get { return bodyMaterialReference.Subject as BodyMaterial; }
			set {
				bodyMaterialReference.Subject = value;
				bodyMaterialReference.Sensitive = false;
			}
		}

		public ConnectionType RestrictConnectionType {
			get { return connectionTypeRreference.Subject as ConnectionType; }
			set {
				connectionTypeRreference.Subject = value;
				connectionTypeRreference.Sensitive = false;
			}
		}

		public Diameter RestrictDiameter {
			get { return diameterCombobox.SelectedItem as Diameter; }
			set {
				diameterCombobox.SelectedItem = value;
				diameterCombobox.Sensitive = false;
			}
		}

		public Pressure RestrictPressure {
			get { return pressureCombobox.SelectedItem as Pressure; }
			set {
				pressureCombobox.SelectedItem = value;
				pressureCombobox.Sensitive = false;
			}
		}

		/// <summary>
		/// Используется только для точного указания модели из кода.
		/// </summary>
		public string RestrictModel { get; set;}

		protected void OnFittingTypeReferenceChanged (object sender, EventArgs e)
		{
			OnRefiltered ();
		}

		protected void OnConnectionTypeRreferenceChanged (object sender, EventArgs e)
		{
			OnRefiltered ();
		}

		protected void OnBodyMaterialReferenceChanged (object sender, EventArgs e)
		{
			OnRefiltered ();
		}

		protected void OnDiameterComboboxChanged (object sender, EventArgs e)
		{
			OnRefiltered ();
		}

		protected void OnPressureComboboxChanged (object sender, EventArgs e)
		{
			OnRefiltered ();
		}
	}
}


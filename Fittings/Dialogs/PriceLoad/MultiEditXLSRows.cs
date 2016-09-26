using System;
using System.Collections.Generic;
using System.Linq;
using Fittings.Domain;
using QSOrmProject;
using QSProjectsLib;

namespace Fittings
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MultiEditXLSRows : WidgetOnDialogBase
	{
		List<ReadingXLSRow> editingList;

		public MultiEditXLSRows()
		{
			this.Build();

			fittingTypeReference.SubjectType = typeof(FittingType);

			connectionTypeRreference.SubjectType = typeof(Fittings.Domain.ConnectionType);

			bodyMaterialReference.SubjectType = typeof(BodyMaterial);
		}

		public void StartEditing(List<ReadingXLSRow> rows)
		{
			if(editingList == null) // Запускаемся первый раз. Нельзя положить в конструктор.
			{
				diameterCombobox.ItemsList = MyOrmDialog.UoW.GetAll<Diameter> ().ToList();
				pressureCombobox.ItemsList = MyOrmDialog.UoW.GetAll<Pressure> ().ToList();
				diameterUnitscombobox.ItemsEnum = typeof(DiameterUnits);
				pressureUnitscombobox.ItemsEnum = typeof(PressureUnits);
			}
			editingList = rows;

			UpdateInfo();

			var onlyEditing = editingList.Where(x => x.Fitting == null).ToList();

			//Проверяем какие поля можем взять под редактирование
			checkModel.Active = AllEqualSetup(onlyEditing, x => x.Code, x => codeEntry.Text = x );
			checkType.Active = AllEqualSetup(onlyEditing, x => x.Name, x => fittingTypeReference.Subject = x);
			diameterCombobox.SelectedItem = onlyEditing.First().DiameterUnits;
			pressureUnitscombobox.SelectedItem = onlyEditing.First().PressureUnits;
			checkDiameter.Active = AllEqualSetup(onlyEditing, x => x.Diameter, x => diameterCombobox.SelectedItem = x);
			checkPressure.Active = AllEqualSetup(onlyEditing, x => x.Pressure, x => pressureCombobox.SelectedItem = x);
			checkConnections.Active = AllEqualSetup(onlyEditing, x => x.ConnectionType, x => connectionTypeRreference.Subject = x);
			checkMaterial.Active = AllEqualSetup(onlyEditing, x => x.BodyMaterial, x => bodyMaterialReference.Subject = x);
			checkComments.Active = AllEqualSetup(onlyEditing, x => x.Note, x => commentTextview.Buffer.Text = x);

			Show();
		}

		private bool AllEqualSetup<TPropery>(List<ReadingXLSRow> rows, Func<ReadingXLSRow, TPropery> prop, Action<TPropery> setToWidget)
		{
			TPropery first = rows.Select(prop).First();
			bool allEqual = rows.All(x => EqualityComparer<TPropery>.Default.Equals(prop(x), first));
			setToWidget(allEqual ? first : default(TPropery));
			return allEqual;
		}

		private void UpdateInfo()
		{
			var editingCount = editingList.Count(x => x.Fitting == null);
			string text = RusNumber.FormatCase(editingCount, "Редактируем {0} строку", "Редактируем {0} строки", "Редактируем {0} строк");
			if(editingCount < editingList.Count)
			{
				text += "\n<span foreground=\"blue\">";
				text += RusNumber.FormatCase(editingList.Count - editingCount, "{0} не редактируется", "{0} не редактируются", "{0} не редактируются");
				text += RusNumber.FormatCase(editingList.Count, "\nВсего выбрано {0}", "\nВсего выбрано {0}", "\nВсего выбрано {0}");
				text += "</span>";
			}
			labelInfo.Markup = text;
		}

		protected void OnCheckModelClicked(object sender, EventArgs e)
		{
			codeEntry.Sensitive = checkModel.Active;
		}

		protected void OnCheckTypeClicked(object sender, EventArgs e)
		{
			fittingTypeReference.Sensitive = checkType.Active;
		}

		protected void OnCheckDiameterClicked(object sender, EventArgs e)
		{
			diameterCombobox.Sensitive = diameterUnitscombobox.Sensitive = checkDiameter.Active;
		}

		protected void OnCheckPressureClicked(object sender, EventArgs e)
		{
			pressureCombobox.Sensitive = pressureUnitscombobox.Sensitive = checkPressure.Active;
		}

		protected void OnCheckConnectionsClicked(object sender, EventArgs e)
		{
			connectionTypeRreference.Sensitive = checkConnections.Active;
		}

		protected void OnCheckMaterialClicked(object sender, EventArgs e)
		{
			bodyMaterialReference.Sensitive = checkMaterial.Active;
		}

		protected void OnCheckCommentsClicked(object sender, EventArgs e)
		{
			commentTextview.Sensitive = checkComments.Active;
		}

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			Hide();
		}

		protected void OnDiameterUnitscomboboxChanged (object sender, EventArgs e)
		{
			switch ((DiameterUnits)diameterUnitscombobox.SelectedItem) {
				case DiameterUnits.inch:
					diameterCombobox.SetRenderTextFunc<Diameter> (x => x.Inch);
					break;
				case DiameterUnits.mm:
					diameterCombobox.SetRenderTextFunc<Diameter> (x => x.Mm);
					break;
			}
		}

		protected void OnPressureUnitscomboboxChanged (object sender, EventArgs e)
		{
			switch ((PressureUnits)pressureUnitscombobox.SelectedItem) {
				case PressureUnits.PN:
					pressureCombobox.SetRenderTextFunc<Pressure> (x => x.Pn);
					break;
				case PressureUnits.Pclass:
					pressureCombobox.SetRenderTextFunc<Pressure> (x => x.Pclass);
					break;
			}
		}

		protected void OnButtonApplyClicked(object sender, EventArgs e)
		{
			foreach(var row in editingList.Where(x => x.Fitting == null))
			{
				if (checkModel.Active)
					row.Code = codeEntry.Text;
				if (checkType.Active)
					row.Name = fittingTypeReference.Subject as FittingType;
				if(checkDiameter.Active)
				{
					row.DiameterUnits = (DiameterUnits)diameterUnitscombobox.SelectedItem;
					row.Diameter = diameterCombobox.SelectedItem as Diameter;
				}
				if(checkPressure.Active)
				{
					row.PressureUnits = (PressureUnits)pressureUnitscombobox.SelectedItem;
					row.Pressure = pressureCombobox.SelectedItem as Pressure;
				}
				if (checkConnections.Active)
					row.ConnectionType = connectionTypeRreference.Subject as Fittings.Domain.ConnectionType;
				if (checkMaterial.Active)
					row.BodyMaterial = bodyMaterialReference.Subject as BodyMaterial;
				if (checkComments.Active)
					row.Note = commentTextview.Buffer.Text;
			}
			Hide();
		}
	}
}


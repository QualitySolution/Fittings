﻿using System;
using System.Collections.Generic;
using Gamma.ColumnConfig;
using Gtk;
using NHibernate.Transform;
using QSOrmProject;
using QSOrmProject.RepresentationModel;
using Fittings.Domain;

namespace Fittings.ViewModel
{
	public class FittingsVM : RepresentationModelEntityBase<Fitting, FittingVMNode>
	{
		#region IRepresentationModel implementation

		public override void UpdateNodes ()
		{
			Fitting fittingAlias = null;
			FittingType typeAlias = null;
			FittingVMNode resultAlias = null;
			Diameter diameterAlias = null;
			Pressure pressureAlias = null;
			ConnectionType connectionTypeAlias = null;
			BodyMaterial bodyMaterialAlias = null;
			Fitting codeAlias = null;
			Fitting noteAlias = null;

			var proxieslist = UoW.Session.QueryOver<Fitting> (() => fittingAlias)
				.JoinAlias (c => c.Name, () => typeAlias)
				.JoinAlias (c => c.Diameter, () => diameterAlias)
				.JoinAlias (c => c.Pressure, () => pressureAlias)
				.JoinAlias (c => c.ConnectionType, () => connectionTypeAlias)
				.JoinAlias (c => c.BodyMaterial, () => bodyMaterialAlias)
				.SelectList(list => list
					.Select(() => fittingAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => typeAlias.NameRus).WithAlias(() => resultAlias.Name)

					.Select(() => diameterAlias.Inch).WithAlias(() => resultAlias.DiameterInch)
					.Select(() => diameterAlias.Mm).WithAlias(() => resultAlias.DiameterMm)
					.Select(() => fittingAlias.DiameterUnits).WithAlias(() => resultAlias.DiameterUnits)

					.Select(() => pressureAlias.Pn).WithAlias(() => resultAlias.PressurePn)
					.Select(() => pressureAlias.Pclass).WithAlias(() => resultAlias.PressurePclass)
					.Select(() => fittingAlias.PressureUnits).WithAlias(() => resultAlias.PressureUnits)

					.Select(() => connectionTypeAlias.NameRus).WithAlias(() => resultAlias.ConnectionType)
					.Select(() => bodyMaterialAlias.NameRus).WithAlias(() => resultAlias.BodyMaterial)
					.Select(() => fittingAlias.Code).WithAlias(() => resultAlias.Code)
					.Select(() => fittingAlias.Note).WithAlias(() => resultAlias.Note)
				)
				.TransformUsing(Transformers.AliasToBean<FittingVMNode>())
				.List<FittingVMNode>();

			SetItemsSource (proxieslist);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig <FittingVMNode>.Create ()
			.AddColumn ("Тип").SetDataProperty (node => node.Name)
			.AddColumn ("Диаметр").SetDataProperty (node => node.Diameter)
			.AddColumn ("Давление").SetDataProperty (node => node.Pressure)
			.AddColumn ("Тип соединения").SetDataProperty (node => node.ConnectionType)
			.AddColumn ("Материал корпуса").SetDataProperty (node => node.BodyMaterial)
			.AddColumn ("Артикул").SetDataProperty (node => node.Code)
			.AddColumn ("Комментарий").SetDataProperty (node => node.Note)
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#endregion

		protected override bool NeedUpdateFunc (Fitting updatedSubject)
		{
			return true;
		}

		public FittingsVM () : this(UnitOfWorkFactory.CreateWithoutRoot()) {}

		public FittingsVM (IUnitOfWork uow)
		{
			this.UoW = uow;
		}
	}

	public class FittingVMNode
	{
		public int Id{ get; set;}

		public string Name{ get; set;}

		public string DiameterMm{ get; set;}
		public string DiameterInch{ get; set;}
		public DiameterUnits DiameterUnits{ get; set;}
		public string Diameter{ get{return DiameterUnits == DiameterUnits.inch ? DiameterInch : DiameterMm;}}

		public string PressurePn{ get; set;}
		public string PressurePclass{ get; set;}
		public PressureUnits PressureUnits{ get; set;}
		public string Pressure{ get{return PressureUnits == PressureUnits.PN ? PressurePn : PressurePclass;}}

		public string ConnectionType{ get; set;}

		public string BodyMaterial{ get; set;}

		public string Code{ get; set;}

		public string Note{ get; set;}
	}
}



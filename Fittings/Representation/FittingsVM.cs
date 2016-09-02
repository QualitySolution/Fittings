using System;
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
			Person personAlias = null;

			var proxieslist = UoW.Session.QueryOver<Fitting> (() => fittingAlias)
				.JoinAlias (c => c.Name, () => typeAlias)
				.JoinAlias (c => c., () => personAlias)
				.Where (() => typeAlias.Id == CounterpartyUoW.Root.Id)
				.SelectList(list => list
					.Select(() => fittingAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => typeAlias.NameRus).WithAlias(() => resultAlias.Name)
					.Select(() => fittingAlias.IssueDate).WithAlias(() => resultAlias.IssueDate)
					.Select(() => fittingAlias.StartDate).WithAlias(() => resultAlias.StartDate)
					.Select(() => fittingAlias.ExpirationDate).WithAlias(() => resultAlias.EndDate)
					.SelectCount(() => personAlias.Id ).WithAlias(() => resultAlias.PeopleCount)
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

		public FittingsVM (IUnitOfWork uow)
		{
			this.UoW = uow;
		}
	}

	public class FittingVMNode
	{
		public int Id{ get; set;}

		public string Name{ get; set;}

		public string Diameter{ get; set;}

		public string Pressure{ get; set;}

		public string ConnectionType{ get; set;}

		public string BodyMaterial{ get; set;}

		public string Code{ get; set;}

		public string Note{ get; set;}
	}
}



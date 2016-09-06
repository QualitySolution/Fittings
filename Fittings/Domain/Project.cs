using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Collections.Generic;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "проекты",
		Nominative = "проект")]
	public class Project: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string customer;

		public virtual string Customer {
			get { return customer; }
			set { SetField (ref customer, value, () => Customer); }
		}

		string projectName;

		public virtual string ProjectName {
			get { return projectName; }
			set { SetField (ref projectName, value, () => ProjectName); }
		}

		IList<ProjectItem> projects = new List<ProjectItem> ();

		[Display (Name = "Проекты")]
		public virtual IList<ProjectItem> Projects {
			get { return projects; }
			set { SetField (ref projects, value, () => Projects); }
		}

		GenericObservableList<ProjectItem> observableProjects;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<ProjectItem> ObservableProjects {
			get {
				if (observableProjects == null)
					observableProjects = new GenericObservableList<ProjectItem> (Projects);
				return observableProjects;
			}
		}

		#endregion

		public Project ()
		{
			
		}
	}
}


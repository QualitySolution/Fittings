using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "пользователи",
		Nominative = "пользователь")]
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

		#endregion

		public Project ()
		{
			
		}
	}
}


using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "прайс листы",
		Nominative = "прайс лист")]
	public class Price: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		DateTime date;

		public virtual DateTime Date {
			get { return date; }
			set { SetField (ref date, value, () => Date); }
		}

		Provider provider;

		public virtual Provider Provider {
			get { return provider; }
			set { SetField (ref provider, value, () => Provider); }
		}

		string comment;

		public virtual string Comment {
			get { return comment; }
			set { SetField (ref comment, value, () => Comment); }
		}

		#endregion

		public Price ()
		{

		}
	}
}


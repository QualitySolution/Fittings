using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "пользователи",
		Nominative = "пользователь")]
	public class Diameter: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string inch;

		public virtual string Inch {
			get { return inch; }
			set { SetField (ref inch, value, () => Inch); }
		}

		string mm;

		public virtual string Mm {
			get { return mm; }
			set { SetField (ref mm, value, () => Mm); }
		}

		#endregion

		public Diameter ()
		{
			
		}
	}

	public enum DiameterUnits {
		[Display (Name = "Дюймы")]
		inch,
		[Display (Name = "Мм")]
		mm
	}

	public class DiameterUnitsStringType : NHibernate.Type.EnumStringType
	{
		public DiameterUnitsStringType () : base (typeof(DiameterUnits))
		{
		}
	}
}


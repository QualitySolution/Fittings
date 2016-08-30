using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "пользователи",
		Nominative = "пользователь")]
	public class Pressure: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string pn;

		public virtual string Pn {
			get { return pn; }
			set { SetField (ref pn, value, () => Pn); }
		}

		string pclass;

		public virtual string Pclass {
			get { return pclass; }
			set { SetField (ref pclass, value, () => Pclass); }
		}

		#endregion

		public Pressure ()
		{
			
		}
	}

	public enum PressureUnits {
		[Display (Name = "PN")]
		PN,
		[Display (Name = "Класс")]
		Pclass
	}

	public class PressureUnitsStringType : NHibernate.Type.EnumStringType
	{
		public PressureUnitsStringType () : base (typeof(PressureUnits))
		{
		}
	}
}


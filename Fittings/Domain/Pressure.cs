using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "давление",
		Nominative = "давление")]
	public class Pressure: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string pn;

		[Required(ErrorMessage = "PN должен быть заполнен")]
		public virtual string Pn {
			get { return pn; }
			set { SetField (ref pn, value, () => Pn); }
		}

		string pclass;

		[Required(ErrorMessage = "Класс должен быть заполнен")]
		public virtual string Pclass {
			get { return pclass; }
			set { SetField (ref pclass, value, () => Pclass); }
		}

		#endregion

		public Pressure ()
		{
			
		}

		#region Функции

		public virtual bool MathPn(string pn)
		{
			if (String.IsNullOrWhiteSpace(Pn) || String.IsNullOrWhiteSpace(pn))
				return false;
			
			return pn.Replace("PN","").Replace("pn", "") == Pn.Replace("PN","").Replace("pn", "");
		}

		#endregion
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


using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using Gamma.Utilities;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "давление",
		Nominative = "давление")]
	public class Pressure: PropertyChangedBase, IDomainObject, IValidatableObject
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

		#region Функции

		public virtual bool MathPn(string pn)
		{
			if (String.IsNullOrWhiteSpace(Pn) || String.IsNullOrWhiteSpace(pn))
				return false;
			
			return pn.Replace("PN","").Replace("pn", "") == Pn.Replace("PN","").Replace("pn", "");
		}

		#endregion

		#region IValidatableObject implementation

		public virtual System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (String.IsNullOrWhiteSpace(Pn) && String.IsNullOrWhiteSpace(Pclass))
				yield return new ValidationResult ("Обязательно должно быть заполнено хотя бы одно из полей: PN или Class.",
					new[] { this.GetPropertyName (o => o.Pn), this.GetPropertyName (o => o.Pclass)});
			
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


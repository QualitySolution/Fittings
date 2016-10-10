using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "диаметр",
		Nominative = "диаметр")]
	public class Diameter: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string inch;

		public virtual string Inch {
			get { return inch; }
			set { SetField (ref inch, value, () => Inch); }
		}

		int mm;

		public virtual int Mm {
			get { return mm; }
			set { 
				if (SetField(ref mm, value, () => Mm))
					DN = String.Format("DN{0}", mm);
				}
		}

		string dn;

		public virtual string DN {
			get { return dn; }
			set { SetField (ref dn, value, () => DN); }
		}

		#endregion

		public Diameter ()
		{
			
		}
	}

	public enum DiameterUnits {
		[Display (Name = "Мм")]
		mm,
		[Display (Name = "Дюймы")]
		inch,
	}

	public class DiameterUnitsStringType : NHibernate.Type.EnumStringType
	{
		public DiameterUnitsStringType () : base (typeof(DiameterUnits))
		{
		}
	}
}


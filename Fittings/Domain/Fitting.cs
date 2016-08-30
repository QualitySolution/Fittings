using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "арматура",
		Nominative = "арматура")]
	public class Fitting: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		FittingType name;

		public virtual FittingType Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		Diameter diameter;

		public virtual Diameter Diameter {
			get { return diameter; }
			set { SetField (ref diameter, value, () => Diameter); }
		}

		DiameterUnits diameterUnits;

		public virtual DiameterUnits DiameterUnits {
			get { return diameterUnits; }
			set { SetField (ref diameterUnits, value, () => DiameterUnits); }
		}

		Pressure pressure;

		public virtual Pressure Pressure {
			get { return pressure; }
			set { SetField (ref pressure, value, () => Pressure); }
		}

		PressureUnits pressureUnits;

		public virtual PressureUnits PressureUnits {
			get { return pressureUnits; }
			set { SetField (ref pressureUnits, value, () => PressureUnits); }
		}

		ConnectionType connectionType;

		public virtual ConnectionType ConnectionType {
			get { return connectionType; }
			set { SetField (ref connectionType, value, () => ConnectionType); }
		}

		BodyMaterial bodyMaterial;

		public virtual BodyMaterial BodyMaterial {
			get { return bodyMaterial; }
			set { SetField (ref bodyMaterial, value, () => BodyMaterial); }
		}

		string code;

		public virtual string Code {
			get { return code; }
			set { SetField (ref code, value, () => Code); }
		}

		string note;

		public virtual string Note {
			get { return note; }
			set { SetField (ref note, value, () => Note); }
		}

		#endregion

		public Fitting ()
		{
			
		}
	}
}


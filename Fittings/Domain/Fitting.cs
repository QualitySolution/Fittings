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

		[Required(ErrorMessage="Тип не заполнен")]
		public virtual FittingType Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		Diameter diameter;
		[Required(ErrorMessage="Диаметр не выбран")]
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
		[Required(ErrorMessage="Давление не выбрано")]
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
		[Required(ErrorMessage="Тип соединения не выбран")]
		public virtual ConnectionType ConnectionType {
			get { return connectionType; }
			set { SetField (ref connectionType, value, () => ConnectionType); }
		}

		BodyMaterial bodyMaterial;
		//[Required(ErrorMessage="Материал корпуса не выбран")]
		public virtual BodyMaterial BodyMaterial {
			get { return bodyMaterial; }
			set { SetField (ref bodyMaterial, value, () => BodyMaterial); }
		}

		string code;
		[Required(ErrorMessage="Артикул не заполнен")]
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

		public virtual string PressureText{ get{
				if (Pressure == null)
					return String.Empty;
				return PressureUnits == PressureUnits.PN ? Pressure.Pn : Pressure.Pclass;}}
		public virtual string DiameterText{ get{
				if (Diameter == null)
					return String.Empty;
				return DiameterUnits == DiameterUnits.inch ? Diameter.Inch : Diameter.Mm;}}

		public Fitting ()
		{
			
		}
	}
}


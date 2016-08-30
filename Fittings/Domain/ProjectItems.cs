using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "пользователи",
		Nominative = "пользователь")]
	public class ProjectItems: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		Project project;

		public virtual Project Project {
			get { return project; }
			set { SetField (ref project, value, () => Project); }
		}

		int sequenceNumber;

		public virtual int SequenceNumber {
			get { return sequenceNumber; }
			set { SetField (ref sequenceNumber, value, () => SequenceNumber); }
		}

		string trpPositions;

		public virtual string TrpPositions {
			get { return trpPositions; }
			set { SetField (ref trpPositions, value, () => TrpPositions); }
		}

		FittingType name;

		public virtual FittingType Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		int amount;

		public virtual int Amount {
			get { return amount; }
			set { SetField (ref amount, value, () => Amount); }
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

		Conductor conductor;

		public virtual Conductor Conductor {
			get { return conductor; }
			set { SetField (ref conductor, value, () => Conductor); }
		}

		string group;

		public virtual string Group {
			get { return group; }
			set { SetField (ref group, value, () => Group); }
		}

		string location;

		public virtual string Location {
			get { return location; }
			set { SetField (ref location, value, () => Location); }
		}

		int temperatureMin;

		public virtual int TemperatureMin {
			get { return temperatureMin; }
			set { SetField (ref temperatureMin, value, () => TemperatureMin); }
		}

		int temperatureMax;

		public virtual int TemperatureMax {
			get { return temperatureMax; }
			set { SetField (ref temperatureMax, value, () => TemperatureMax); }
		}

		string comment;

		public virtual string Comment {
			get { return comment; }
			set { SetField (ref comment, value, () => Comment); }
		}

		Fitting fitting;

		public virtual Fitting Fitting {
			get { return fitting; }
			set { SetField (ref fitting, value, () => Fitting); }
		}

		#endregion

		public ProjectItems ()
		{
			
		}
	}
}


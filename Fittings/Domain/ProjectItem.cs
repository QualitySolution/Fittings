using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "элементы проекта",
		Nominative = "элемент проекта")]
	public class ProjectItem: PropertyChangedBase, IDomainObject
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

		int amount;

		public virtual int Amount {
			get { return amount; }
			set { SetField (ref amount, value, () => Amount); }
		}


		decimal fittingPrice;

		public virtual decimal FittingPrice {
			get { return fittingPrice; }
			set { SetField (ref fittingPrice, value, () => FittingPrice); }
		}

		PriceСurrency priceCurrency;

		public virtual PriceСurrency PriceCurrency {
			get { return priceCurrency; }
			set { SetField (ref priceCurrency, value, () => PriceCurrency); }
		}

		PriceItem selectedPriceItem;

		public virtual PriceItem SelectedPriceItem {
			get { return selectedPriceItem; }
			set { SetField (ref selectedPriceItem, value, () => SelectedPriceItem); }
		}

		Conductor conductor;

		public virtual Conductor Conductor {
			get { return conductor; }
			set { SetField (ref conductor, value, () => Conductor); }
		}

		string prGroup;

		public virtual string PrGroup {
			get { return prGroup; }
			set { SetField (ref prGroup, value, () => PrGroup); }
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

		public ProjectItem ()
		{
			
		}
	}
}


using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "строки прайса",
		Nominative = "строка прайса")]
	public class PriceItems: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		Fitting fitting;

		public virtual Fitting Fitting {
			get { return fitting; }
			set { SetField (ref fitting, value, () => Fitting); }
		}

		Price price;

		public virtual Price Price {
			get { return price; }
			set { SetField (ref price, value, () => Price); }
		}

		PriceСurrency currency;

		public virtual PriceСurrency Currency {
			get { return currency; }
			set { SetField (ref currency, value, () => Currency); }
		}

		decimal cost;

		public virtual decimal Cost {
			get { return cost; }
			set { SetField (ref cost, value, () => Cost); }
		}

		#endregion

		public PriceItems ()
		{

		}
	}

	public enum PriceСurrency {
		[Display (Name = "Доллар")]
		USD,
		[Display (Name = "Евро")]
		EUR,
		[Display (Name = "Рубль")]
		RUB
	}

	public class PriceСurrencyStringType : NHibernate.Type.EnumStringType
	{
		public PriceСurrencyStringType () : base (typeof(PriceСurrency))
		{
		}
	}
}


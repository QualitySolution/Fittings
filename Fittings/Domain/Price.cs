using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Collections.Generic;

namespace Fittings.Domain
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "прайс листы",
		Nominative = "прайс лист")]
	public class Price: PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		DateTime date;

		public virtual DateTime Date {
			get { return date; }
			set { SetField (ref date, value, () => Date); }
		}

		Provider provider;

		public virtual Provider Provider {
			get { return provider; }
			set { SetField (ref provider, value, () => Provider); }
		}

		string comment;

		public virtual string Comment {
			get { return comment; }
			set { SetField (ref comment, value, () => Comment); }
		}

		IList<PriceItem> prices = new List<PriceItem> ();

		[Display (Name = "Прайс")]
		public virtual IList<PriceItem> Prices {
			get { return prices; }
			set { SetField (ref prices, value, () => Prices); }
		}
			
		GenericObservableList<PriceItem> observablePrices;
		//FIXME Костыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<PriceItem> ObservablePrices {
			get {
				if (observablePrices == null)
					observablePrices = new GenericObservableList<PriceItem> (Prices);
				return observablePrices;
			}
		}

		#endregion


		public Price ()
		{

		}

		public virtual void AddItem(Fitting fitting, PriceСurrency currency , decimal cost = 0){
			var item = new PriceItem {
				Price = this,
				Fitting = fitting,
				Currency = currency,
				Cost = cost 
			};
			ObservablePrices.Add (item);
		}
	}
}


using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class PriceItemsMap : ClassMap<PriceItem>
	{
		public PriceItemsMap ()
		{
			Table ("price_items");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			References (x => x.Fitting).Column ("fitting_id");
			References (x => x.Price).Column ("price_id");
			Map (x => x.Currency).Column ("price_units");
			Map (x => x.Cost).Column ("cost");
		}
	}
}
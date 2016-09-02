using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class PriceMap : ClassMap<Price>
	{
		public PriceMap ()
		{
			Table ("price");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.Date).Column ("date");
			References (x => x.Provider).Column ("provider_id");
			Map (x => x.Comment).Column ("comment");
		}
	}
}
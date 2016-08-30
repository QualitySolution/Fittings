using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class ProviderMap : ClassMap<Provider>
	{
		public ProviderMap ()
		{
			Table ("provider");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.Name).Column ("name");
		}
	}
}
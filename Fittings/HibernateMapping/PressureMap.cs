using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class PressureMap : ClassMap<Pressure>
	{
		public PressureMap ()
		{
			Table ("pressure");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.Pn).Column ("PN");
			Map (x => x.Pclass).Column ("Pclass");
		}
	}
}
using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class ConductorMap : ClassMap<Conductor>
	{
		public ConductorMap ()
		{
			Table ("conductor");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.NameRus).Column ("name_rus");
			Map (x => x.NameEng).Column ("name_eng");
		}
	}
}
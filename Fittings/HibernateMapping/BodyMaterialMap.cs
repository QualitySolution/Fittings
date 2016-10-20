using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class BodyMaterialMap : ClassMap<BodyMaterial>
	{
		public BodyMaterialMap ()
		{
			Table ("body_material");
			Not.LazyLoad();

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.NameRus).Column ("name_rus");
			Map (x => x.NameEng).Column ("name_eng");
		}
	}
}
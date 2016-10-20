using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class FittingTypeMap : ClassMap<FittingType>
	{
		public FittingTypeMap ()
		{
			Table ("fitting_type");
			Not.LazyLoad();

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.NameRus).Column ("name_rus");
			Map (x => x.NameEng).Column ("name_eng");
			Map (x => x.ModelCode).Column ("model_code");
		}
	}
}
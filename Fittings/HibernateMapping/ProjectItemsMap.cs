using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class ProjectItemsMap : ClassMap<ProjectItem>
	{
		public ProjectItemsMap ()
		{
			Table ("project_items");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();

			Map (x => x.SequenceNumber).Column ("sequence_number");
			Map (x => x.TrpPositions).Column ("TRP_position");
			Map (x => x.Amount).Column ("amount");
			Map (x => x.PrGroup).Column ("row_group");
			Map (x => x.Location).Column ("location");
			Map (x => x.TemperatureMin).Column ("temperature_min");
			Map (x => x.TemperatureMax).Column ("temperature_max");
			Map (x => x.Comment).Column ("comment");
			Map(x => x.FittingPrice).Column("price");
			Map(x => x.PriceCurrency).Column("currency").CustomType<PriceСurrencyStringType>();

			References (x => x.Conductor).Column ("conductor_id");
			References (x => x.Fitting).Column ("fitting_id");
			References (x => x.Project).Column ("project_id");
			References (x => x.SelectedPriceItem).Column ("price_item_id");
		}
	}
}
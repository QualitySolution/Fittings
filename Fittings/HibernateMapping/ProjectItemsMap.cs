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
			References (x => x.Project).Column ("project_id");
			Map (x => x.SequenceNumber).Column ("sequence_number");
			Map (x => x.TrpPositions).Column ("TRP_position");
			References (x => x.Name).Column ("name_id");
			Map (x => x.Amount).Column ("number");
			References (x => x.Diameter).Column ("diametr_id");
			Map (x => x.DiameterUnits).Column ("diametr_units").CustomType<DiameterUnitsStringType>();
			References (x => x.Pressure).Column ("pressure_id");
			Map (x => x.PressureUnits).Column ("pressure_units").CustomType<PressureUnitsStringType>();
			References (x => x.ConnectionType).Column ("connection_type_id");
			References (x => x.Conductor).Column ("conductor_id");
			Map (x => x.Group).Column ("group");
			Map (x => x.Location).Column ("location");
			Map (x => x.TemperatureMin).Column ("temperature_min");
			Map (x => x.TemperatureMax).Column ("temperature_max");
			Map (x => x.Comment).Column ("comment");
			References (x => x.Fitting).Column ("fitting_id");

		}
	}
}
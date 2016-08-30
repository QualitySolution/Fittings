using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class FittingsMap : ClassMap<Fitting>
	{
		public FittingsMap ()
		{
			Table ("fittings");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			References (x => x.Name).Column ("fitting_name_id");
			References (x => x.Diameter).Column ("diametr_id");
			Map (x => x.DiameterUnits).Column ("diametr_units").CustomType<DiameterUnitsStringType>();
			References (x => x.Pressure).Column ("pressure_id");
			Map (x => x.PressureUnits).Column ("pressure_units").CustomType<PressureUnitsStringType>();
			References (x => x.ConnectionType).Column ("connection_type_id");
			References (x => x.BodyMaterial).Column ("body_material_id");
			Map (x => x.Code).Column ("code");
			Map (x => x.Note).Column ("note");

		}
	}
}
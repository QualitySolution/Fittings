using System;
using System.Collections.Generic;
using System.Linq;
using Fittings.Domain;
using QSOrmProject;

namespace Fittings
{

	public class ReadingXLSWorkClass
	{
		public IUnitOfWork UoW;

		public IList<Diameter> Diameters;
		public IList<Pressure> Pressures;

		public void ParseDiameter(string dn, ReadingXLSRow row)
		{
			dn = dn.Replace("DN","").Replace("dn", "");
			var found = Diameters.FirstOrDefault(x => x.Mm == dn);
			if (found != null)
			{
				row.Diameter = found;
				row.DiameterUnits = DiameterUnits.mm;
				return;
			}

			row.Diameter = Diameters.FirstOrDefault(x => x.Inch == dn);
			if (row.Diameter != null)
				row.DiameterUnits = DiameterUnits.inch;
			return;
		}

		public void ParsePressure(string pn, ReadingXLSRow row)
		{
			var found = Pressures.FirstOrDefault(x => x.MathPn(pn));
			if (found != null)
			{
				row.Pressure = found;
				row.PressureUnits = PressureUnits.PN;
				return;
			}

			row.Pressure = Pressures.FirstOrDefault(x => x.Pclass == pn);
			if (row.Pressure != null)
				row.PressureUnits = PressureUnits.Pclass;
			return;
		}

	}
}

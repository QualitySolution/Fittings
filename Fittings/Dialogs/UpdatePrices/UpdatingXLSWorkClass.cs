using System;
using System.Collections.Generic;
using System.Linq;
using Fittings.Domain;
using QSOrmProject;

namespace Fittings
{
	public class UpdatingXLSWorkClass
	{
		public IUnitOfWork UoW;

		public PriceСurrency Currency;

		public IList<Diameter> Diameters;

		public void ParseDiameter(string dn, UpdatingXLSRow row)
		{
			dn = dn.Replace("DN","").Replace("dn", "");
			int mm;
			if (int.TryParse(dn, out mm))
			{
				var found = Diameters.FirstOrDefault(x => x.Mm == mm);
				if (found != null)
				{
					row.Diameter = found;
					return;
				}
			}

			row.Diameter = Diameters.FirstOrDefault(x => x.Inch == dn);
			return;
		}
	}
}

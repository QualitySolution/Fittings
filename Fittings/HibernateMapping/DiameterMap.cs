﻿using FluentNHibernate.Mapping;
using Fittings.Domain;

namespace Fittings.HMap
{
	public class DiameterMap : ClassMap<Diameter>
	{
		public DiameterMap ()
		{
			Table ("diameter");

			Id (x => x.Id).Column ("id").GeneratedBy.Native ();
			Map (x => x.Inch).Column ("inch");
			Map (x => x.Mm).Column ("mm");
		}
	}
}
using QSOrmProject;
using Fittings.Domain;
using System.Collections.Generic;

namespace Fittings.Repository
{
	public static class FittingRepository
	{
		public static IList<Fitting> GetFittings (IUnitOfWork uow, string model, Diameter dn)
		{
			return uow.Session.QueryOver<Fitting>()
				.Where(f => f.Code == model)
				.Where(f => f.Diameter == dn)
				.List();			
		}
	}
}


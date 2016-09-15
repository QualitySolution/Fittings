using QSOrmProject;
using Fittings.Domain;

namespace Fittings.Repository
{
	public static class PriceRepository
	{
		public static PriceItem GetLastPriceItem (IUnitOfWork uow, Fitting fitting, Provider provider)
		{
			PriceItem pricePriceItemAlias = null;
			Price pricePriceAlias = null;

			var query = uow.Session.QueryOver<PriceItem>(() => pricePriceItemAlias)
				.JoinAlias(c => c.Price, () => pricePriceAlias)
				.Where(() => pricePriceItemAlias.Fitting.Id == fitting.Id);
			
			if (provider != null)
				query.Where(() => pricePriceAlias.Provider.Id == provider.Id);

			return query
				.OrderBy(() => pricePriceAlias.Date).Desc
				.Take(1)
				.SingleOrDefault();
		}
	}
}


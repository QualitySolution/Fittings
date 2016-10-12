using QSOrmProject;
using Fittings.Domain;
using System.Collections.Generic;
using NHibernate.Criterion;

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

		/// <summary>
		/// Получаем цены уникальные для каждого поставщика.
		/// </summary>
		public static IList<PriceItem> GetLastPrices (IUnitOfWork uow, Fitting[] fittings)
		{
			PriceItem queryPriceItemAlias = null;
			PriceItem subqueryPriceItemAlias = null;
			Price queryPriceAlias = null;
			Price subqueryPriceAlias = null;

			var subquery = QueryOver.Of<PriceItem>(() => subqueryPriceItemAlias)
				.JoinAlias(c => c.Price, () => subqueryPriceAlias)
				.Where(() => subqueryPriceItemAlias.Fitting.Id == queryPriceItemAlias.Fitting.Id)
				.Where(() => subqueryPriceAlias.Provider.Id == queryPriceAlias.Provider.Id)
				.OrderBy(() => subqueryPriceAlias.Date).Desc
				.Select(x => x.Id)
				.Take(1);

			var query = uow.Session.QueryOver<PriceItem>(() => queryPriceItemAlias)
				.JoinAlias(c => c.Price, () => queryPriceAlias)
				.Where(() => queryPriceItemAlias.Fitting.IsIn(fittings))
				.WithSubquery.WhereProperty(x => x.Id).Eq(subquery);

			return query.List();
		}

	}
}


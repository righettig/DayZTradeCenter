using System;
using System.Collections.Generic;
using System.Web.Http;
using DayZTradeCenter.DomainModel.Interfaces;

namespace DayZTradeCenter.UI.Web.Controllers.Api
{
    public class TrendsController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrendsController"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <exception cref="System.ArgumentNullException">provider</exception>
        public TrendsController(IAnalyticsProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            _provider = provider;
        }

        public Tuple<IEnumerable<TrendsResult>, IEnumerable<TrendsResult>> Get(int itemId)
        {
            return new Tuple<IEnumerable<TrendsResult>, IEnumerable<TrendsResult>>(
                _provider.GetDailyTrendsFor(itemId, TrendsType.W),
                _provider.GetDailyTrendsFor(itemId, TrendsType.H));
        }

        [Route("api/items/most_wanted")]
        public IEnumerable<ItemDetails> GetMostWantedItem()
        {
            return _provider.GetMostWantedItem();
        }

        [Route("api/items/most_offered")]
        public IEnumerable<ItemDetails> GetMostOfferedItem()
        {
            return _provider.GetMostOfferedItem();
        }

        private readonly IAnalyticsProvider _provider;
    }
}

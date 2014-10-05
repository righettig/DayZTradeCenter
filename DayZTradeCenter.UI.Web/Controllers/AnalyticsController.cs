using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.UI.Web.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsController"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <exception cref="System.ArgumentNullException">provider</exception>
        public AnalyticsController(IAnalyticsProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            _provider = provider;
        }

        // GET: Analytics
        public ActionResult Index()
        {
            var vm = new AnalyticsViewModel
            {
                MostWantedItems = _provider.GetMostWantedItem(),
                MostOfferedItems = _provider.GetMostOfferedItem(),
                Items = new SelectList(_provider.GetAllItems(), "Id", "Name")
            };

            // by default shows the trends of the #1 most wanted item.
            var item =
                vm.MostWantedItems.First().Item.Id;

            var wTrends =
                GetDailyTrendsFor(item, TrendsType.W).ToArray();

            var hTrends =
                GetDailyTrendsFor(item, TrendsType.H).ToArray();

            var chart = new Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories =
                        wTrends
                            .Select(t => t.Date.ToShortDateString())
                            .Union(
                                hTrends.Select(t => t.Date.ToShortDateString()))
                            .Distinct()
                            .ToArray()
                })
                .SetSeries(new[]
                {
                    new Series
                    {
                        Name = "W",
                        Data =
                            new Data(wTrends.Select(t => t.Count).Cast<object>().ToArray())
                    },
                    new Series
                    {
                        Name = "H",
                        Data =
                            new Data(hTrends.Select(t => t.Count).Cast<object>().ToArray())
                    }
                });

            vm.Chart = chart;

            return View(vm);
        }

        public IEnumerable<TrendsResult> GetDailyTrendsFor(int itemId, TrendsType type)
        {
            return _provider.GetDailyTrendsFor(itemId, type);
        }

        private readonly IAnalyticsProvider _provider;
    }
}
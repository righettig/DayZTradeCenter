using System;
using System.Collections.Generic;
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

            var chart = new Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories =
                        new[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
                })
                .SetSeries(new Series
                {
                    Data =
                        new Data(new object[] {29.9, 71.5, 106.4, 129.2, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4})
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
using System;
using System.Diagnostics;
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
        public ActionResult Index(int? itemId)
        {
            var items = _provider.GetAllItems().ToArray();

            var vm = new AnalyticsViewModel(
                _provider.GetMostWantedCategory(),
                _provider.GetMostOfferedCategory(),
                _provider.GetMostWantedSubcategory(),
                _provider.GetMostOfferedSubcategory())
            {
                MostWantedItems = _provider.GetMostWantedItem(),
                MostOfferedItems = _provider.GetMostOfferedItem(),

                Items = new SelectList(items, "Id", "Name")
            };

            // by default shows the trends of the #1 most wanted item (if present).
            var mostWantedItem = vm.MostWantedItems.FirstOrDefault();

            if (mostWantedItem != null)
            {
                vm.ItemId = mostWantedItem.Item.Id;
            }
            else // ... or the trends of the first item.
            {
                Debug.Assert(items.FirstOrDefault() != null, "There are no items in the db!");

                vm.ItemId = items.First().Id;
            }

            vm.Chart = CreateChart(
                itemId ?? vm.ItemId); // if specified, shows the trends of the specified item.

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Analytics", vm);
            }

            return View(vm);
        }

        public PartialViewResult GetDailyTrendsFor(int itemId)
        {
            var chart = CreateChart(itemId);

            return PartialView("_TrendsChart", chart);
        }

        private Highcharts CreateChart(int itemId)
        {
            var wTrends =
                _provider.GetDailyTrendsFor(itemId, TrendsType.W).ToArray();

            var hTrends =
                _provider.GetDailyTrendsFor(itemId, TrendsType.H).ToArray();

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

            return chart;
        }

        private readonly IAnalyticsProvider _provider;
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel.Services
{
    public class AnalyticsProvider : IAnalyticsProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsProvider"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <param name="itemsRepository">The items repository.</param>
        /// <exception cref="System.ArgumentNullException">
        /// tradesRepository
        /// or
        /// itemsRepository
        /// </exception>
        public AnalyticsProvider(IRepository<Trade> tradesRepository, IRepository<Item> itemsRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            if (itemsRepository == null)
            {
                throw new ArgumentNullException("itemsRepository");
            }

            _tradesRepository = tradesRepository;
            _itemsRepository = itemsRepository;
        }

        public IEnumerable<ItemDetails> GetMostWantedItem()
        {
            return GetMostListedItem(trade => trade.Want);
        }

        public IEnumerable<ItemDetails> GetMostWantedItem(DateTime start, DateTime end)
        {
            return GetMostListedItem(
                trade => trade.Want, 
                trade => trade.CreationDate >= start && trade.CreationDate <= end);
        }

        public IEnumerable<ItemDetails> GetMostOfferedItem()
        {
            return GetMostListedItem(trade => trade.Have);
        }

        public IEnumerable<ItemDetails> GetMostOfferedItem(DateTime start, DateTime end)
        {
            return GetMostListedItem(
                trade => trade.Have,
                trade => trade.CreationDate >= start && trade.CreationDate <= end);
        }

        public IEnumerable<TrendsResult> GetDailyTrendsFor(int itemId, TrendsType type)
        {
            IEnumerable<TrendsResult> result;

            switch (type)
            {
                case TrendsType.H:
                    result =
                        GroupByDateAndFilterByItem(itemId, g => g.SelectMany(j => j.Have));
                    break;

                case TrendsType.W:
                    result =
                        GroupByDateAndFilterByItem(itemId, g => g.SelectMany(j => j.Want));
                    break;

                case TrendsType.Both:
                    result =
                        GroupByDateAndFilterByItem(itemId,
                            g =>
                                g.SelectMany(k => k.Have)
                                    .Union(g.SelectMany(j => j.Want)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException("type");
            }

            return result;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemsRepository.GetAll();
        }

        private IEnumerable<ItemDetails> GetMostListedItem(
            Func<Trade, IEnumerable<TradeDetails>> collectionSelector)
        {
            return GetMostListedItem(collectionSelector, trade => true);
        }

        private IEnumerable<ItemDetails> GetMostListedItem(
            Func<Trade, IEnumerable<TradeDetails>> collectionSelector, 
            Func<Trade, bool> rangeFn)
        {
            return
                _tradesRepository
                    .GetAll()
                    .Where(rangeFn)

                    // NB: solves the issue with "There is already an open DataReader associated with this Command .." 
                    .ToArray()
                    
                    .SelectMany(collectionSelector).Select(d => d.Item)
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(
                        grp => new ItemDetails
                        {
                            Item = grp.Key,
                            Count = grp.Count()
                        });
        }

        private IEnumerable<TrendsResult> GroupByDateAndFilterByItem(
            int itemId, Func<IGrouping<DateTime?, Trade>, IEnumerable<TradeDetails>> collectionSelector)
        {
            return
                _tradesRepository.GetAll()
                    .GroupBy(trade => DbFunctions.TruncateTime(trade.CreationDate))

                    // NB: solves the issue "Only parameterless constructors and initializers are supported in LINQ to Entities."
                    .ToArray()

                    .Select(grp =>
                        new TrendsResult(
                            grp.Key.Value,
                            itemId,
                            collectionSelector(grp).Count(t => t.Item.Id == itemId)));
        }

        #region Private fields

        private readonly IRepository<Trade> _tradesRepository;
        private readonly IRepository<Item> _itemsRepository;

        #endregion
    }
}
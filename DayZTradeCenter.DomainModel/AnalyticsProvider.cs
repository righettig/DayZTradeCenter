﻿using System;
using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Interfaces;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
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
            return
                _tradesRepository
                    .GetAll()
                    .SelectMany(trade => trade.Want).Select(d => d.Item)
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(
                        grp => new ItemDetails
                        {
                            Item = grp.Key,
                            Count = grp.Count()
                        });
        }

        public IEnumerable<ItemDetails> GetMostOfferedItem()
        {
            return
                _tradesRepository
                    .GetAll()
                    .SelectMany(trade => trade.Have).Select(d => d.Item)
                    .GroupBy(i => i)
                    .OrderByDescending(grp => grp.Count())
                    .Select(
                        grp => new ItemDetails
                        {
                            Item = grp.Key,
                            Count = grp.Count()
                        });
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

        private IEnumerable<TrendsResult> GroupByDateAndFilterByItem(
            int itemId, Func<IGrouping<DateTime, Trade>, IEnumerable<TradeDetails>> collectionSelector)
        {
            return
                from trade in _tradesRepository.GetAll()
                group trade by trade.CreationDate.Date
                into grp
                select new TrendsResult
                {
                    Date = grp.Key,
                    ItemId = itemId,
                    Count =
                        collectionSelector(grp).Count(t => t.Item.Id == itemId)
                };
        }

        #region Private fields

        private readonly IRepository<Trade> _tradesRepository;
        private readonly IRepository<Item> _itemsRepository;

        #endregion
    }
}
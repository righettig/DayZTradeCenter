using System;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Migrations;
using DayZTradeCenter.DomainModel.Services;
using DayZTradeCenter.Modules.Core;
using DayZTradeCenter.Modules.Test;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.InMemory
{
    /// <summary>
    /// Configures the repositories as in-memory repositories.
    /// </summary>
    public class InMemoryRepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<EventInfo>>().ToConstant(
                new InMemoryRepository<EventInfo>());

            Bind<IRepository<Item>>().ToConstant(
                InsertDefaultItems(new InMemoryRepository<Item>()));

            Bind<IRepository<Trade>>().ToConstant(
                InsertDefaultTrades(new InMemoryRepository<Trade>()));
        }

        #region Helper methods

        /// <summary>
        /// Inserts the default items.
        /// </summary>
        /// <param name="repository">The repository.</param>
        private static IRepository<Item> InsertDefaultItems(IRepository<Item> repository)
        {
            foreach (var item in ItemsHelper.Items)
            {
                repository.Insert(item);
            }

            repository.SaveChanges();

            return repository;
        }

        /// <summary>
        /// Inserts the default trades.
        /// </summary>
        /// <param name="repository">The repository.</param>
        private static IRepository<Trade> InsertDefaultTrades(IRepository<Trade> repository)
        {
            // test trade for the test user1 with a couple of offers
            var trade = new Trade
            {
                CreationDate = DateTime.Now,
                Owner = new ApplicationUser {Id = DefaultUsers.TestUser1.UserId}
            };
            trade.Have.Add(new TradeDetails(ItemsHelper.Pitchfork, 3));
            trade.Want.Add(new TradeDetails(ItemsHelper.SKS, 1));

            var rnd = new Random();
            trade.Offers.Add(CreateFakeUser(rnd, DefaultUsers.TestUser2.UserId));
            trade.Offers.Add(CreateFakeUser(rnd, DefaultUsers.TestUser3.UserId));

            repository.Insert(trade);
            repository.SaveChanges();

            return repository;
        }

        private static ApplicationUser CreateFakeUser(Random rnd, string userId)
        {
            var result = new FakeUser(rnd.Next(11)) {Id = userId};
            return result;
        }

        #endregion
    }
}

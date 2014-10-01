using System;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities;
using DayZTradeCenter.DomainModel.Identity.Migrations;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    /// <summary>
    /// Configures <see cref="IRepository{Trade}"/> as an in-memory repository.
    /// </summary>
    /// <remarks>
    /// Fake trades are defined with the owners' reputation randomly assigned.
    /// </remarks>
    public class TradesRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new InMemoryRepository<Trade>();


            // test trade for the test user1 with a couple of offers
            var owner = new Mock<IApplicationUser>();
            owner.SetupGet(m => m.Id).Returns(DefaultUsers.TestUser1.UserId);

            var trade = new Trade
            {
                CreationDate = DateTime.Now,
                Owner = owner.Object
            };
            trade.Have.Add(new TradeDetails(ItemsHelper.Pitchfork, 3));
            trade.Want.Add(new TradeDetails(ItemsHelper.SKS, 1));

            var rnd = new Random();
            trade.Offers.Add(CreateFakeUser(rnd, DefaultUsers.TestUser2.UserId));
            trade.Offers.Add(CreateFakeUser(rnd, DefaultUsers.TestUser3.UserId));

            repository.Insert(trade);


            repository.SaveChanges();

            Bind<IRepository<Trade>>().ToConstant(repository);
        }

        private static IApplicationUser CreateFakeUser(Random rnd, string userId)
        {
            var result = new Mock<IApplicationUser>();
            result.SetupGet(m => m.Id).Returns(userId);
            result.Setup(m => m.GetReputation()).Returns(rnd.Next(11));

            return result.Object;
        }
    }
}
using System;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    public class TradesRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new InMemoryRepository<Trade>();


            var owner = new Mock<IApplicationUser>();
            owner.SetupGet(m => m.Id).Returns("1234567890");

            var trade = new Trade
            {
                CreationDate = DateTime.Now,
                Owner = owner.Object
            };
            trade.Have.Add(new TradeDetails(ItemsHelper.Mosin, 1));
            trade.Want.Add(new TradeDetails(ItemsHelper.Tent, 2));

            repository.Insert(trade);


            // test trade for the current user with a couple of offers
            owner = new Mock<IApplicationUser>();
            owner.SetupGet(m => m.Id).Returns("245f522a-d489-40e5-838b-b89773aeff68");

            trade = new Trade
            {
                CreationDate = DateTime.Now,
                Owner = owner.Object
            };
            trade.Have.Add(new TradeDetails(ItemsHelper.Pitchfork, 3));
            trade.Want.Add(new TradeDetails(ItemsHelper.SKS, 1));

            var rnd = new Random();
            trade.Offers.Add(CreateFakeUser(rnd));
            trade.Offers.Add(CreateFakeUser(rnd));

            repository.Insert(trade);


            repository.SaveChanges();

            Bind<IRepository<Trade>>().ToConstant(repository);
        }

        private static IApplicationUser CreateFakeUser(Random rnd)
        {
            var result = new Mock<IApplicationUser>();
            result.SetupGet(m => m.Id).Returns(rnd.Next(int.MaxValue).ToString());
            result.Setup(m => m.GetReputation()).Returns(rnd.Next(11));

            return result.Object;
        }
    }
}
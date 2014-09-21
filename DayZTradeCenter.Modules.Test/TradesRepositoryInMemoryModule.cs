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
            trade.Have.Add(ItemsHelper.Mosin);
            trade.Want.Add(ItemsHelper.Tent);

            repository.Insert(trade);
            repository.SaveChanges();

            Bind<IRepository<Trade>>().ToConstant(repository);
        }
    }
}
using System;
using System.Linq;
using DayZTradeCenter.DomainModel;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Modules.Test
{
    public class TradesRepositoryTestModule : NinjectModule
    {
        public override void Load()
        {
            var random = new Random();

            var trades = new[]
            {
                CreateFakeTrade(random),
                CreateFakeTrade(random),
                CreateFakeTrade(random)
            }.AsQueryable();

            var tradesRepository = new Mock<IRepository<Trade>>();
            tradesRepository.Setup(m => m.GetAll()).Returns(trades);

            Bind<IRepository<Trade>>().ToConstant(tradesRepository.Object);
        }

        private static Trade CreateFakeTrade(Random random)
        {
            var result = new Trade
            {
                Id = random.Next(1000)
            };

            return result;
        }
    }
}

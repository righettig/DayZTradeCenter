using System.Linq;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Modules.Test
{
    public class TradesRepositoryTestModule : NinjectModule
    {
        public override void Load()
        {
            var trades = new[]
            {
                CreateFakeTrade1(),
                CreateFakeTrade2()
            }.AsQueryable();

            var tradesRepository = new Mock<IRepository<Trade>>();
            tradesRepository.Setup(m => m.GetAll()).Returns(trades);

            Bind<IRepository<Trade>>().ToConstant(tradesRepository.Object);
        }

        private static Trade CreateFakeTrade1()
        {
            var result = new Trade {Id = 1};

            result.Have.Add(ItemsHelper.Mosin);
            result.Have.Add(ItemsHelper.SKS);
            result.Want.Add(ItemsHelper.Tent);

            SetOwner(result, 7);

            return result;
        }

        private static Trade CreateFakeTrade2()
        {
            var result = new Trade { Id = 2 };
            
            result.Have.Add(ItemsHelper.Crowbar);
            result.Want.Add(ItemsHelper.Pitchfork);

            SetOwner(result, 1);

            return result;
        }

        private static void SetOwner(Trade result, int reputation)
        {
            var owner = new Mock<IApplicationUser>();
            owner.Setup(m => m.GetReputation()).Returns(reputation);

            result.Owner = owner.Object;
        }
    }
}

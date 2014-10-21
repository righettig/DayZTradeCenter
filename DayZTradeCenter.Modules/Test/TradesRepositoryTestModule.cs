using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.Modules.Core;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Modules.Test
{
    /// <summary>
    /// Configures <see cref="IRepository{Trade}"/> as a stub repository
    /// with only the GetAll method defined.
    /// </summary>
    /// <remarks>
    /// Fake trades are defined with the owners' reputation randomly assigned.
    /// </remarks>
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

            result.Have.Add(new TradeDetails(ItemsHelper.Mosin, 3, ItemCondition.Pristine));
            result.Have.Add(new TradeDetails(ItemsHelper.SKS, 1, ItemCondition.Worn));
            result.Want.Add(new TradeDetails(ItemsHelper.Tent, 1, ItemCondition.Damaged));

            SetOwner(result, 7);

            return result;
        }

        private static Trade CreateFakeTrade2()
        {
            var result = new Trade {Id = 2};

            result.Have.Add(new TradeDetails(ItemsHelper.Crowbar, 1, ItemCondition.Damaged));
            result.Want.Add(new TradeDetails(ItemsHelper.Pitchfork, 1, ItemCondition.Pristine));

            SetOwner(result, 1);

            return result;
        }

        /// <summary>
        /// Sets the owner for the specified trade with the given reputation.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="reputation">The reputation.</param>
        private static void SetOwner(Trade result, int reputation)
        {
            var owner = new FakeUser(reputation);

            result.Owner = owner;
        }
    }
}

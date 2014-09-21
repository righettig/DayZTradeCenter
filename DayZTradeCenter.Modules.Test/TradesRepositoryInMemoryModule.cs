using DayZTradeCenter.DomainModel;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    public class TradesRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Trade>>().ToConstant(
                new InMemoryRepository<Trade>());
        }
    }
}
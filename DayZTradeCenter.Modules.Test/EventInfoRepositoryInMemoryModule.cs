using DayZTradeCenter.DomainModel;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    public class EventInfoRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new InMemoryRepository<EventInfo>();

            Bind<IRepository<EventInfo>>().ToConstant(repository);
        }
    }
}

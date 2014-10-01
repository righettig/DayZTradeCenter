using DayZTradeCenter.DomainModel;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    /// <summary>
    /// Configures <see cref="IRepository{EventInfo}"/> as an in-memory repository.
    /// </summary>
    public class EventInfoRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new InMemoryRepository<EventInfo>();

            Bind<IRepository<EventInfo>>().ToConstant(repository);
        }
    }
}

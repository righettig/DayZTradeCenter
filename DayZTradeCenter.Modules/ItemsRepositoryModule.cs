using System.Data.Entity;
using DayZTradeCenter.DomainModel;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.EF;

namespace DayZTradeCenter.Modules
{
    /// <summary>
    /// Configures <see cref="IRepository{Item}"/> as a SQL repository.
    /// </summary>
    public class ItemsRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DayZTradeCenterDbContext>().WhenInjectedInto<SqlRepository<Item>>();

            // TODO: InRequestScope
            Bind<IRepository<Item>>().To<SqlRepository<Item>>();
        }
    }
}

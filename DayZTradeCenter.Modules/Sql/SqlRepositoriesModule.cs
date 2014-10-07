using System.Data.Entity;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Web.Common;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.EF;

namespace DayZTradeCenter.Modules.Sql
{
    /// <summary>
    /// Configures the repositories as SQL repositories.
    /// </summary>
    public class SqlRepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();

            Bind<IRepository<EventInfo>>().To<SqlRepository<EventInfo>>().InRequestScope();
            Bind<IRepository<Item>>().To<SqlRepository<Item>>().InRequestScope();
            Bind<IRepository<Trade>>().To<SqlRepository<Trade>>().InRequestScope();

            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().InRequestScope();
        }
    }
}

using DayZTradeCenter.DomainModel;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    /// <summary>
    /// Configures <see cref="IRepository{Item}"/> as an in-memory repository with a few default items.
    /// </summary>
    public class ItemsRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new InMemoryRepository<Item>();

            InsertDefaultItems(repository);

            Bind<IRepository<Item>>().ToConstant(repository);
        }

        /// <summary>
        /// Inserts the default items.
        /// </summary>
        /// <param name="repository">The repository.</param>
        private static void InsertDefaultItems(IRepository<Item> repository)
        {
            foreach (var item in ItemsHelper.Items)
            {
                repository.Insert(item);
            }

            repository.SaveChanges();
        }
    }
}
using DayZTradeCenter.DomainModel;
using Ninject.Modules;
using rg.GenericRepository.Core;
using rg.GenericRepository.Impl.InMemory;

namespace DayZTradeCenter.Modules.Test
{
    public class ItemsRepositoryInMemoryModule : NinjectModule
    {
        public override void Load()
        {
            var repository = new InMemoryRepository<Item>();

            InsertDefaultItems(repository);

            Bind<IRepository<Item>>().ToConstant(repository);
        }

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
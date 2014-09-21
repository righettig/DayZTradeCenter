using System.Linq;
using DayZTradeCenter.DomainModel;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Modules.Test
{
    public class ItemsRepositoryTestModule : NinjectModule
    {
        public override void Load()
        {
            var items = ItemsHelper.Items.ToArray();

            var itemsRepository = new Mock<IRepository<Item>>();
            
            itemsRepository
                .Setup(m => m.GetAll())
                .Returns(items.AsQueryable());

            itemsRepository
                .Setup(m => m.GetSingle(It.IsAny<int>()))
                .Returns((int i) => items[i]);

            Bind<IRepository<Item>>().ToConstant(itemsRepository.Object);
        }
    }
}

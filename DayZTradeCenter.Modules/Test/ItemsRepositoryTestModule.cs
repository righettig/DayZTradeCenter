using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.Modules.Core;
using Moq;
using Ninject.Modules;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Modules.Test
{
    /// <summary>
    /// Configures <see cref="IRepository{Item}"/> as a stub repository 
    /// with only the GetAll and GetSingle methods defined.
    /// </summary>
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

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
            var items = new[]
            {
                new Item {Id = 0, Name = "Mosin"},
                new Item {Id = 1, Name = "SKS"},
                new Item {Id = 2, Name = "Tent"},
                new Item {Id = 3, Name = "Pitchfork"},
                new Item {Id = 4, Name = "Crowbar"}
            };

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

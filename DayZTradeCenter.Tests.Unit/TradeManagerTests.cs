using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Tests.Unit
{
    [TestClass]
    public class TradeManagerTests
    {
        [TestMethod]
        public void UsersCanCreateUpTo3ActiveTrades()
        {
            // Arrange
            var tradesRepository =
                new Mock<IRepository<Trade>>();

            tradesRepository
                .Setup(m => m.GetAll())
                .Returns(Enumerable.Empty<Trade>().AsQueryable());

            var mgr = new TradeManager(
                tradesRepository.Object, CreateItemRepository(), CreateUserStore());


            // Act
            var result = mgr.CanCreateTrade("foo");


            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UsersCantCreateMoreThan3ActiveTrades()
        {
            // Arrange
            var tradesRepository =
                new Mock<IRepository<Trade>>();

            var user = new ApplicationUser {Id = "foo"};

            var trade = new Trade { Owner = user };

            tradesRepository.Setup(m => m.GetAll()).Returns(new[]
            {
                trade, trade, trade
            }.AsQueryable());

            var mgr = new TradeManager(
                tradesRepository.Object, CreateItemRepository(), CreateUserStore());


            // Act
            var result = mgr.CanCreateTrade("foo");


            // Assert
            Assert.IsFalse(result);
        }

        #region Helper methods

        private static IRepository<Item> CreateItemRepository()
        {
            return new Mock<IRepository<Item>>().Object;
        }

        private static IUserStore<ApplicationUser> CreateUserStore()
        {
            return new Mock<IUserStore<ApplicationUser>>().Object;
        }

        #endregion
    }
}

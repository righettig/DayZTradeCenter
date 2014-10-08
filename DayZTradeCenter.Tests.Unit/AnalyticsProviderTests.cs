using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.Tests.Unit
{
    [TestClass]
    public class AnalyticsProviderTests
    {
        [TestMethod]
        public void GetWScore()
        {
            // Arrange
            var mgr = CreateAnalyticsProvider();


            // Act
            var w1 = mgr.GetWScore(1);
            var w2 = mgr.GetWScore(2);
            var w3 = mgr.GetWScore(3);


            // Assert
            Assert.AreEqual(2f/3f, w1);
            Assert.AreEqual(0, w2);
            Assert.AreEqual(1, w3);
        }

        [TestMethod]
        public void GetRScore()
        {
            // Arrange
            var mgr = CreateAnalyticsProvider();


            // Act
            var r1 = mgr.GetRScore(new[] {1, 2});
            var r2 = mgr.GetRScore(new[] {1, 2, 3});
            var r3 = mgr.GetRScore(new[] {2});
            var r4 = mgr.GetRScore(new[] {3});


            // Assert
            Assert.AreEqual(2f/3f, r1);
            //Assert.AreEqual(1f + 2f/3f, r2);
            Assert.AreEqual(0, r3);
            Assert.AreEqual(1, r4);
        }

        private static AnalyticsProvider CreateAnalyticsProvider()
        {
            var i1 = new Item {Id = 1, Name = "i1"};
            var i2 = new Item {Id = 2, Name = "i2"};
            var i3 = new Item {Id = 3, Name = "i3"};

            var t1 = new Trade();
            t1.Want.Add(new TradeDetails(i1, 1));
            t1.Want.Add(new TradeDetails(i3, 1));

            var t2 = new Trade();
            t2.Want.Add(new TradeDetails(i3, 1));

            var t3 = new Trade();
            t3.Want.Add(new TradeDetails(i1, 1));
            t3.Want.Add(new TradeDetails(i3, 1));

            var trades = new Mock<IRepository<Trade>>();
            trades.Setup(m => m.GetAll()).Returns(new[] {t1, t2, t3}.AsQueryable());

            var items = new Mock<IRepository<Item>>().Object;

            var mgr = new AnalyticsProvider(trades.Object, items);
            return mgr;
        }
    }
}

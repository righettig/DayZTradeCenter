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

        [TestMethod]
        public void GetTScore()
        {
            // Arrange
            var mgr = CreateAnalyticsProvider();


            // Act
            var t1 = mgr.GetTScore(1);
            var t2 = mgr.GetTScore(2);
            var t3 = mgr.GetTScore(3);


            // Assert
            Assert.AreEqual(0, t1);
            Assert.AreEqual(2f / 3f, t2);
            Assert.AreEqual(0, t3);
        }

        private static AnalyticsProvider CreateAnalyticsProvider()
        {
            var i1 = new Item {Id = 1, Name = "i1"};
            var i2 = new Item {Id = 2, Name = "i2"};
            var i3 = new Item {Id = 3, Name = "i3"};

            var t1 = new Trade {Id = 1};
            t1.Want.Add(new TradeDetails(i1, 1, ItemCondition.Pristine));
            t1.Want.Add(new TradeDetails(i3, 1, ItemCondition.Pristine));
            t1.Have.Add(new TradeDetails(i2, 1, ItemCondition.Pristine));

            var t2 = new Trade {Id = 2};
            t2.Want.Add(new TradeDetails(i3, 1, ItemCondition.Pristine));
            t2.Have.Add(new TradeDetails(i1, 1, ItemCondition.Pristine));
            t2.Have.Add(new TradeDetails(i2, 1, ItemCondition.Pristine));

            var t3 = new Trade {Id = 3};
            t3.Want.Add(new TradeDetails(i1, 1, ItemCondition.Pristine));
            t3.Want.Add(new TradeDetails(i3, 1, ItemCondition.Pristine));
            t3.Have.Add(new TradeDetails(i2, 1, ItemCondition.Pristine));

            var trades = new Mock<IRepository<Trade>>();
            trades.Setup(m => m.GetAll()).Returns(new[] {t1, t2, t3}.AsQueryable());

            trades.Setup(m => m.GetSingle(1)).Returns(t1);
            trades.Setup(m => m.GetSingle(2)).Returns(t2);
            trades.Setup(m => m.GetSingle(3)).Returns(t3);

            var items = new Mock<IRepository<Item>>().Object;

            return new AnalyticsProvider(trades.Object, items);
        }
    }
}

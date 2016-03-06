using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomShare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using ExpectedObjects;


namespace TomShare.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {

        /// <summary>
        /// 存放測試資料
        /// </summary>
        private Order[] _orders;

        /// <summary>
        /// 初始化資料，即使Order內部的值被修改也不會影響其它測試
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _orders = new Order[] {
                new Order() { Id = 1, Cost = 1, Revenue = 11, SellPrice = 21 },
                new Order() { Id = 2, Cost = 2, Revenue = 12, SellPrice = 22 },
                new Order() { Id = 3, Cost = 3, Revenue = 13, SellPrice = 23 },
                new Order() { Id = 4, Cost = 4, Revenue = 14, SellPrice = 24 },
                new Order() { Id = 5, Cost = 5, Revenue = 15, SellPrice = 25 },
                new Order() { Id = 6, Cost = 6, Revenue = 16, SellPrice = 26 },
                new Order() { Id = 7, Cost = 7, Revenue = 17, SellPrice = 27 },
                new Order() { Id = 8, Cost = 8, Revenue = 18, SellPrice = 28 },
                new Order() { Id = 9, Cost = 9, Revenue = 19, SellPrice = 29 },
                new Order() { Id =10, Cost =10, Revenue = 20, SellPrice = 30 },
                new Order() { Id =11, Cost =11, Revenue = 21, SellPrice = 31 },
            };
        }
        
        #region 測試可能情況計算
        [TestMethod]
        public void Sum_3筆1組_計算Cost_應為6和15和24和21()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => x.Cost;
            var sources = _orders;
            var groupCount = 3;
            var expected = new List<int> { 6, 15, 24, 21 };
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount).ToList();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Sum_4筆1組_計算Revenue_應為50和66和60()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => x.Revenue;
            var sources = _orders;
            var groupCount = 4;
            var expected = new List<int> { 50, 66, 60 };
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount).ToList();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Sum_12筆1組_計算Cost_應為66()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => x.Cost;
            var sources = _orders;
            var groupCount = 12;
            var expected = new List<int> { 66 };
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount).ToList();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Sum_20筆1組_計算Cost_應為66()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => x.Cost;
            var sources = _orders;
            var groupCount = 20;
            var expected = new List<int> { 66 };
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount).ToList();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Sum_groupCount傳入1_應為1和2和3和4和5和6和7和8和9和10和11()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => x.Cost;
            Order[] sources = _orders;
            var groupCount = 1;
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount).ToArray();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        #endregion

        #region 測試來源參數驗證
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Sum_getValueFunc傳入空值_應擲出例外()
        {
            //Arrange
            Func<Order, int> findValueFunc = null;
            var sources = _orders;
            var groupCount = 4;
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Sum_sources傳入空值_應擲出例外()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => 1;
            Order[] sources = null;
            var groupCount = 4;
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount);

            //Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sum_sources陣列中有null值_應擲出例外()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => 1;
            Order[] sources = new Order[] {
                new Order() { Id = 1, Cost = 1, Revenue = 11, SellPrice = 21 },
                null,
                new Order() { Id = 2, Cost = 2, Revenue = 12, SellPrice = 22 }
            };
            var groupCount = 2;
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount);

            //Assert
        }
        #endregion

        #region 傳入非預期數值
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sum_groupCount傳入0_應擲出例外()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => 1;
            Order[] sources = _orders;
            var groupCount = 0;
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount);

            //Assert
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Sum_groupCount為負1_應擲出例外()
        {
            //Arrange
            Func<Order, int> findValueFunc = (x) => 1;
            Order[] sources = _orders;
            var groupCount = -1;
            var target = new OrderService();
            //Act
            var actual = target.Sum(findValueFunc, sources, groupCount);

            //Assert
        }
        #endregion

    }
}
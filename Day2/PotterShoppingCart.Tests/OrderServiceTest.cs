using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotterShoppingCart.Tests
{
    //    User Story
    //哈利波特一到五冊熱潮正席捲全球，世界各地的孩子都為之瘋狂。
    //出版社為了慶祝Agile Tour第一次在台灣舉辦，決定訂出極大的優惠，來回饋給為了小孩四處奔波買書的父母親們。
    //定價的方式如下：
    //1. 一到五集的哈利波特，每一本都是賣100元
    //2. 如果你從這個系列買了兩本不同的書，則會有5%的折扣
    //3. 如果你買了三本不同的書，則會有10%的折扣
    //4. 如果是四本不同的書，則會有20%的折扣
    //5. 如果你一次買了整套一到五集，恭喜你將享有25%的折扣
    //6. 需要留意的是，如果你買了四本書，其中三本不同，第四本則是重複的，
    //   那麼那三本將享有10%的折扣，但重複的那一本，則仍須100元。
    // 你的任務是，設計一個哈利波特的購物車，能提供最便宜的價格給這些爸爸媽媽。



    [TestClass]
    public class OrderServiceTest
    {
        //Feature: PotterShoppingCart
        //    In order to 提供最便宜的價格給來買書的爸爸媽媽
        //    As a 佛心的出版社老闆
        //I want to 設計一個哈利波特的購物車

        //Scenario: 第一集買了一本100元，其他都沒買，價格應為100*1=100元
        [TestMethod]
        public void Checkout_買Book1一本_Price應為100元()
        {
            //Arrange
            var book1 = new Product() //Book1產品內容
            {
                Name = "哈利波特第一集",
                Price = 100
            };
            var orderInfoList = new OrderDetail[] {
                new OrderDetail() //訂單細項，買Book1 1本
                {
                    Product = book1, //購買Book1
                    Quantity = 1 //購買數量
                }
            };

            var target = new OrderService(); //訂單計算服務
            var expected = 100; //預期結果為100元

            //Act
            //進行結帳動作，產生訂單，並計算結果
            Order actual = target.Checkout(orderInfoList);

            //Assert
            //檢查訂單的Total是否為預期結果
            Assert.AreEqual(expected, actual.Total); 
        }
        //Scenario: 第一集買了一本，第二集也買了一本，價格應為100*2*0.95=190
        [TestMethod]
        public void Checkout_買Book1一本和Book2一本_Price應為190元()
        {
            //Arrange
            var book1 = new Product() //Book1產品內容
            {
                Name = "哈利波特第一集",
                Price = 100
            };
            var book2 = new Product() //Book2產品內容
            {
                Name = "哈利波特第二集",
                Price = 100
            };
            var orderInfoList = new OrderDetail[] {
                new OrderDetail() //訂單細項，買Book1 1本
                {
                    Product = book1, //購買Book1
                    Quantity = 1 //購買數量
                },
                new OrderDetail()
                {
                    Product = book2, //購買Book2
                    Quantity = 1 //購買數量
                }
            };

            var target = new OrderService(); //訂單計算服務
            var expected = 190; //預期結果為190元

            //Act
            //進行結帳動作，產生訂單，並計算結果
            Order actual = target.Checkout(orderInfoList);

            //Assert
            //檢查訂單的Total是否為預期結果
            Assert.AreEqual(expected, actual.Total);
        }

        //Scenario: Scenario: 一二三集各買了一本，價格應為100*3*0.9=270
        [TestMethod]
        public void Checkout_買Book1一本和Book2一本和Book3一本_Price應為270元()
        {
            //Arrange
            var book1 = new Product() //Book1產品內容
            {
                Name = "哈利波特第一集",
                Price = 100
            };
            var book2 = new Product() //Book2產品內容
            {
                Name = "哈利波特第二集",
                Price = 100
            };
            var book3 = new Product() //Book3產品內容
            {
                Name = "哈利波特第三集",
                Price = 100
            };
            var orderInfoList = new OrderDetail[] {
                new OrderDetail() //訂單細項，買Book1 1本
                {
                    Product = book1, //購買Book1
                    Quantity = 1 //購買數量
                },
                new OrderDetail()
                {
                    Product = book2, //購買Book2
                    Quantity = 1 //購買數量
                },
                new OrderDetail()
                {
                    Product = book3, //購買Book3
                    Quantity = 1 //購買數量
                }
            };

            var target = new OrderService(); //訂單計算服務
            var expected = 270; //預期結果

            //Act
            //進行結帳動作，產生訂單，並計算結果
            Order actual = target.Checkout(orderInfoList);

            //Assert
            //檢查訂單的Total是否為預期結果
            Assert.AreEqual(expected, actual.Total);
        }

        //Scenario: 一二三四集各買了一本，價格應為100*4*0.8=320
        [TestMethod]
        public void Checkout_買Book1一本和Book2一本和Book3一本和Book4一本_Price應為320元()
        {
            //Arrange
            var book1 = new Product() //Book1產品內容
            {
                Name = "哈利波特第一集",
                Price = 100
            };
            var book2 = new Product() //Book2產品內容
            {
                Name = "哈利波特第二集",
                Price = 100
            };
            var book3 = new Product() //Book3產品內容
            {
                Name = "哈利波特第三集",
                Price = 100
            };
            var book4 = new Product() //Book4產品內容
            {
                Name = "哈利波特第四集",
                Price = 100
            };
            var orderInfoList = new OrderDetail[] {
                new OrderDetail() //訂單細項，買Book1 1本
                {
                    Product = book1, //購買Book1
                    Quantity = 1 //購買數量
                },
                new OrderDetail()
                {
                    Product = book2, //購買Book2
                    Quantity = 1 //購買數量
                },
                new OrderDetail()
                {
                    Product = book3, //購買Book3
                    Quantity = 1 //購買數量
                },
                new OrderDetail()
                {
                    Product = book4, //購買Book4
                    Quantity = 1 //購買數量
                },
            };

            var target = new OrderService(); //訂單計算服務
            var expected = 320; //預期結果

            //Act
            //進行結帳動作，產生訂單，並計算結果
            Order actual = target.Checkout(orderInfoList);

            //Assert
            //檢查訂單的Total是否為預期結果
            Assert.AreEqual(expected, actual.Total);
        }

        //Scenario: 一次買了整套，一二三四五集各買了一本，價格應為100*5*0.75=375
        [TestMethod]
        public void Checkout_買Book1一本和Book2一本和Book3一本和Book4一本和Book5一本_Price應為375元()
        {
            //Arrange
            var book1 = new Product()
            {
                Name = "哈利波特第一集",
                Price = 100
            };
            var book2 = new Product()
            {
                Name = "哈利波特第二集",
                Price = 100
            };
            var book3 = new Product()
            {
                Name = "哈利波特第三集",
                Price = 100
            };
            var book4 = new Product()
            {
                Name = "哈利波特第四集",
                Price = 100
            };
            var book5 = new Product()
            {
                Name = "哈利波特第五集",
                Price = 100
            };
            var orderInfoList = new OrderDetail[] {
                new OrderDetail() //訂單細項
                {
                    Product = book1,
                    Quantity = 1
                },
                new OrderDetail()
                {
                    Product = book2,
                    Quantity = 1
                },
                new OrderDetail()
                {
                    Product = book3,
                    Quantity = 1
                },
                new OrderDetail()
                {
                    Product = book4,
                    Quantity = 1
                },
                new OrderDetail()
                {
                    Product = book5,
                    Quantity = 1
                }
            };

            var target = new OrderService(); //訂單計算服務
            var expected = 375; //預期結果

            //Act
            //進行結帳動作，產生訂單，並計算結果
            Order actual = target.Checkout(orderInfoList);

            //Assert
            //檢查訂單的Total是否為預期結果
            Assert.AreEqual(expected, actual.Total);
        }
    }
}

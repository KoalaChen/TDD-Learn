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
    }
}

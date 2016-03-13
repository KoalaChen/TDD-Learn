using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterShoppingCart
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// 結帳並產生訂單
        /// </summary>
        /// <param name="orderDetails">選擇的商品及相關資訊</param>
        /// <returns>訂單</returns>
        public Order Checkout(IEnumerable<OrderDetail> orderDetails)
        {
            //訂單
            Order order = new Order();
            order.Details = orderDetails;
            //計算，以產品分組，計算產品數量
            var productQuantities = orderDetails
                .GroupBy(a => a.Product) //分組
                .Select(a => 
                    new KeyValuePair<Product, int>(
                        a.Key, //每一個產品
                        a.Sum(detail => detail.Quantity) //總計產品數量
                    )
                );
            //傳遞給規則的相關參數
            var context = new CalculateRuleContext()
            {
                OrderDetails = orderDetails,
                RemainProducts = productQuantities.ToList() //剩餘計算的產品項目，剛開始為全部
            };
            //每次篩選商品數量 > 0的部分，當沒有計算項目時，則停止執行
            while (context.RemainProducts.Any())
            {
                //篩選剩餘數量>0的商品
                context.RemainProducts = context.RemainProducts.Where(a => a.Value > 0).ToArray();

                //規則越前面優先執行
                //有5組以上不同商品則折價，扣除已計算的項目
                new DifferentItemDiscountRule(reachCount: 5, discountRate: 0.75).Calculate(context);
                //有4組不同商品則折價，扣除已計算的項目
                new DifferentItemDiscountRule(reachCount: 4, discountRate: 0.8).Calculate(context);
                //有3組不同商品則折價，扣除已計算的項目
                new DifferentItemDiscountRule(reachCount: 3, discountRate: 0.9).Calculate(context);
                //有2組不同商品則折價，扣除已計算的項目
                new DifferentItemDiscountRule(reachCount: 2, discountRate: 0.95).Calculate(context);
                //有1組則原價，扣除已計算的項目
                new DifferentItemDiscountRule(reachCount: 1, discountRate: 1).Calculate(context);
            }
            //設定計算結果
            order.Total = context.Total;
            return order;
        }
    }
}
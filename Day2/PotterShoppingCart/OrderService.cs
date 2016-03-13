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
            double result = 0;
            //計算，以產品分組，計算產品數量
            var productQuantities = orderDetails
                .GroupBy(a => a.Product) //分組
                .Select(a => 
                    new KeyValuePair<Product, int>(
                        a.Key, //每一個產品
                        a.Sum(detail => detail.Quantity) //總計產品數量
                    )
                );

            IEnumerable<KeyValuePair<Product, int>> remainProductQuantities = productQuantities.ToList();
            //每次篩選商品數量 > 0的部分，當沒有計算項目時，則停止執行
            while (remainProductQuantities.Any())
            {
                //計算
                remainProductQuantities = remainProductQuantities.Where(a => a.Value > 0).ToList();

                //小記
                int subTotal = remainProductQuantities.Sum(a => a.Key.Price);
                
                //規則越前面優先執行
                if (remainProductQuantities.Count() >= 5)
                {
                    //有5組以上不同商品則折價
                    result += subTotal * 0.75;
                }
                if (remainProductQuantities.Count() == 4)
                {
                    //有4組不同商品則折價
                    result += subTotal * 0.8;
                }
                if (remainProductQuantities.Count() == 3)
                {
                    //有3組不同商品則折價
                    result += subTotal * 0.9;
                }
                if (remainProductQuantities.Count() == 2)
                {
                    //有2組不同商品則折價
                    result += subTotal * 0.95;
                }
                if (remainProductQuantities.Count() == 1)
                {
                    //有1組則原價
                    result += subTotal;
                }
                //扣除已計算的商品數量
                remainProductQuantities = remainProductQuantities
                    .Select(group => new KeyValuePair<Product, int>(group.Key, group.Value - 1));
            }
            //設定計算結果
            order.Total = result;
            return order;
        }
    }
}
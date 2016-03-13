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
        /// 計算規則列表
        /// </summary>
        public IList<ICalculateRule> Rules { get; set; } = new List<ICalculateRule>();

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
                var currentRules = this.Rules.ToArray();
                //執行規則
                foreach (var rule in currentRules)
                    rule.Calculate(context);
            }
            //設定計算結果
            order.Total = context.Total;
            return order;
        }
    }
}
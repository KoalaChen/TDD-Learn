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
        public IList<ICalculateRule> CacluateRules { get; set; } = new List<ICalculateRule>();

        /// <summary>
        /// 結帳並產生訂單
        /// </summary>
        /// <param name="orderDetails">選擇的商品及相關資訊</param>
        /// <returns>訂單</returns>
        public Order Checkout(IEnumerable<OrderDetail> orderDetails)
        {
            if (orderDetails == null)
                throw new ArgumentNullException(nameof(orderDetails));
            if (!orderDetails.Any())
                throw new ArgumentException($"{nameof(orderDetails)}無任何詳細訂單資訊");
            if (orderDetails.Any(detail => detail == null))
                throw new ArgumentException($"{nameof(orderDetails)}有一或多個項目為null");

            //計算，以產品分組，計算產品數量
            var productQuantities = CalculateQuantityByProduct(orderDetails);
            //傳遞給規則的相關參數
            var context = CreateRuleContext(orderDetails, productQuantities);
            //執行規則
            ExecuteRule(context);
            //建立訂單並設定總價
            var order = new Order()
            {
                Details = orderDetails,
                Total = context.Total
            };
            return order;
        }
        /// <summary>
        /// 以產品為群組，計算產品的相關數量
        /// </summary>
        protected virtual IEnumerable<KeyValuePair<Product, int>> CalculateQuantityByProduct(IEnumerable<OrderDetail> orderDetails) {
            if(orderDetails == null)
                throw new ArgumentNullException(nameof(orderDetails));
            //計算，以產品分組，計算產品數量
            var productQuantities = orderDetails
                .GroupBy(a => a.Product) //分組
                .Select(a =>
                    new KeyValuePair<Product, int>(
                        a.Key, //每一個產品
                        a.Sum(detail => detail.Quantity) //總計產品數量
                    )
                );
            return productQuantities;
        }
        /// <summary>
        /// 建立規則參數物件
        /// </summary>
        /// <param name="orderDetails">原始訂購資料</param>
        /// <param name="productQuantities">每項產品的數量列表</param>
        /// <returns></returns>
        protected virtual CalculateRuleContext CreateRuleContext(
            IEnumerable<OrderDetail> orderDetails, 
            IEnumerable<KeyValuePair<Product,int>> productQuantities)
        {
            if (orderDetails == null)
                throw new ArgumentNullException(nameof(orderDetails));
            if (productQuantities == null)
                throw new ArgumentNullException(nameof(productQuantities));
            //傳遞給規則的相關參數
            var context = new CalculateRuleContext()
            {
                OrderDetails = orderDetails,
                RemainProducts = productQuantities.ToList() //剩餘計算的產品項目，剛開始為全部
            };
            return context;
        }
        /// <summary>
        /// 執行規則
        /// </summary>
        /// <param name="context">規則參數傳遞</param>
        protected virtual void ExecuteRule(CalculateRuleContext context)
        {
            //每次篩選商品數量 > 0的部分，當沒有計算項目時，則停止執行
            while (context.RemainProducts.Any())
            {
                var currentRules = this.CacluateRules.ToArray();
                //執行規則
                foreach (var rule in currentRules)
                    rule.Calculate(context);
            }
        }
    }
}
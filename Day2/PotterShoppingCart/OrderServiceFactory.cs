using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotterShoppingCart
{
    /// <summary>
    /// 訂單服務工廠
    /// </summary>
    public class OrderServiceFactory
    {
        /// <summary>
        /// 建立並設定服務
        /// </summary>
        /// <returns></returns>
        public OrderService Build()
        {
            var service = new OrderService();
            var rules = new CaculateRuleFactory().Build().ToList();
            service.Rules = rules;
            return service;
        }
    }
}

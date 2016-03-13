using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotterShoppingCart
{
    /// <summary>
    /// 計算規則建立工廠
    /// </summary>
    public class CaculateRuleFactory
    {
        /// <summary>
        /// 建立規則
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICalculateRule> Build()
        {
            var list = new List<ICalculateRule>();
            //規則越前面優先執行
            //有5組以上不同商品則折價，扣除已計算的項目
            list.Add(new DifferentItemDiscountRule(reachCount: 5, discountRate: 0.75));
            //有4組不同商品則折價，扣除已計算的項目
            list.Add(new DifferentItemDiscountRule(reachCount: 4, discountRate: 0.8));
            //有3組不同商品則折價，扣除已計算的項目
            list.Add(new DifferentItemDiscountRule(reachCount: 3, discountRate: 0.9));
            //有2組不同商品則折價，扣除已計算的項目
            list.Add(new DifferentItemDiscountRule(reachCount: 2, discountRate: 0.95));
            //有1組則原價，扣除已計算的項目
            list.Add(new DifferentItemDiscountRule(reachCount: 1, discountRate: 1));
            return list;
        }
    }
}

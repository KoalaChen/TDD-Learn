using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotterShoppingCart
{
    /// <summary>
    /// 計算不同項目的打折優惠
    /// </summary>
    public class DifferentItemDiscountRule : ICalculateRule
    {
        /// <summary>
        /// 有幾筆不同則折價
        /// </summary>
        private readonly int _reachCount;
        /// <summary>
        /// 折價折扣率
        /// </summary>
        private readonly double _discountRate;

        /// <summary>
        /// 建立計算不同項目的打折優惠之執行個體
        /// </summary>
        /// <param name="reachCount">達到某個條件則執行</param>
        /// <param name="discountRate">有幾筆不同則折價</param>
        public DifferentItemDiscountRule(int reachCount, double discountRate)
        {
            if (reachCount < 1)
                throw new ArgumentException($"{nameof(reachCount)}不能小於1");
            if (!(0 < discountRate && discountRate <= 1))
                throw new ArgumentException($"{nameof(discountRate)}必須介於0~1之間的小數點，最大為1");
            this._reachCount = reachCount;
            this._discountRate = discountRate;
        }

        public void Calculate(CalculateRuleContext context)
        {
            //篩選剩餘數量>0的商品
            context.RemainProducts = context.RemainProducts.Where(a => a.Value > 0).ToArray();
            //若為達條件則跳出
            if (context.RemainProducts.Count() < _reachCount)
                return;
            //達到條件時往下執行
            //每項商品加總小計
            var subTotal = context.RemainProducts.Sum(a => a.Key.Price);
            //打折
            double discountSubTotal = subTotal * _discountRate;
            //總計相加
            context.Total += discountSubTotal;
            //扣除已計算的商品數量
            context.RemainProducts = context.RemainProducts
                .Select(group => 
                new KeyValuePair<Product, int>(
                    group.Key, group.Value - 1
                ));
        }
    }
}

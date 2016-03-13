using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotterShoppingCart
{
    /// <summary>
    /// 計算規則
    /// </summary>
    public interface ICalculateRule
    {
        /// <summary>
        /// 計算價格
        /// </summary>
        /// <param name="context">計算時可用的相關資料</param>
        void Calculate(CalculateRuleContext context);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotterShoppingCart
{
    /// <summary>
    /// 計算規則期間傳遞參數物件
    /// </summary>
    public class CalculateRuleContext
    {
        /// <summary>
        /// 原始詳細訂單資料
        /// </summary>
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        /// <summary>
        /// 剩餘計算
        /// </summary>
        public IEnumerable<KeyValuePair<Product, int>> RemainProducts { get; set; }
        /// <summary>
        /// 目前總共計算金額，預設為0
        /// </summary>
        public double Total { get; set; } = 0;
    }
}

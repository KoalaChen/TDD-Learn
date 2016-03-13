using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterShoppingCart
{
    /// <summary>
    /// 訂單資訊
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 訂單詳細資料
        /// </summary>
        public IEnumerable<OrderDetail> Details { get; internal set; }
        /// <summary>
        /// 售價
        /// </summary>
        public double Total { get; internal set; }
    }
}
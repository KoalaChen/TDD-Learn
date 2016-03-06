using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomShare
{
    /// <summary>
    /// 訂單資訊
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// 收入
        /// </summary>
        public int Revenue { get; set; }
        /// <summary>
        /// 銷售價格
        /// </summary>
        public int SellPrice { get; set; }
    }
}

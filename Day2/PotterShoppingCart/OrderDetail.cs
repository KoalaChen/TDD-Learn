namespace PotterShoppingCart
{
    /// <summary>
    /// 訂單詳細資料
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// 關聯的產品
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 產品訂購數量
        /// </summary>
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Product={Product} Quantity={Quantity}";
        }
    }
}
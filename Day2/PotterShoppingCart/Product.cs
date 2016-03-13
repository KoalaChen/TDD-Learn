namespace PotterShoppingCart
{
    /// <summary>
    /// 產品資訊
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 產品名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 售價
        /// </summary>
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Name={Name} Price={Price}";
        }
    }
}
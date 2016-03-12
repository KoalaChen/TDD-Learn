namespace PotterShoppingCart
{
    public class Order
    {
        public OrderDetail[] Details { get; internal set; }
        public int Total { get; set; }
    }
}
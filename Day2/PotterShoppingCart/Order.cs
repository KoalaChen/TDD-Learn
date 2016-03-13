namespace PotterShoppingCart
{
    public class Order
    {
        public OrderDetail[] Details { get; internal set; }
        public double Total { get; set; }
    }
}
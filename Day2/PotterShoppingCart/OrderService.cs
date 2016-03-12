using System;

namespace PotterShoppingCart
{
    public class OrderService
    {
        public OrderService()
        {
        }

        public Order Checkout(OrderDetail[] orderDetails)
        {
            Order order = new Order();
            order.Details = orderDetails;
            var result = 0;
            foreach (var item in orderDetails)
            {
                result += item.Product.Price * item.Quantity;
            }
            order.Total = result;
            return order;
        }
    }
}
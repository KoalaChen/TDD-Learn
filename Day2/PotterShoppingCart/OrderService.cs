using System;
using System.Linq;

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
            double result = 0;
            foreach (var item in orderDetails)
            {
                result += item.Product.Price * item.Quantity;
            }
            var groupResult = orderDetails.GroupBy(a => a.Product);
            if (groupResult.Count() == 2) //若有兩本不同，就打95折
            {
                result = result * 0.95;
            }
            order.Total = result;
            return order;
        }
    }
}
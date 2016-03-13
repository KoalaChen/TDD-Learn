using System;
using System.Collections.Generic;
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
            //訂單
            Order order = new Order();
            order.Details = orderDetails;
            double result = 0;
            //計算，依照Product分組，加總每個Product的數量
            var productQuantityGroup = orderDetails
                .GroupBy(a => a.Product) //依照Product分組
                .Select(a => 
                    new KeyValuePair<Product, int>(
                        a.Key, //每一個產品
                        a.Select(detail => detail.Quantity) //抽取詳細資料的產品數量
                        .Aggregate((quantity1, quantity2)  //數量加總
                            => quantity1+ quantity2))
                );

            IEnumerable<KeyValuePair<Product, int>> tmpProductQuantityGroup = productQuantityGroup.ToList();
            //每次篩選商品數量>0的部分

            while (tmpProductQuantityGroup.Any())
            {
                tmpProductQuantityGroup = tmpProductQuantityGroup.Where(a => a.Value > 0).ToList();

                //小記
                int subTotal;
                if (tmpProductQuantityGroup.Any())
                    subTotal = tmpProductQuantityGroup
                        .Select(a => a.Key.Price)
                        .Aggregate((price1, price2) => price1 + price2);
                else
                    subTotal = 0;
                //規則越前面優先執行
                if (tmpProductQuantityGroup.Count() >= 5)
                {
                    result += subTotal * 0.75; //計算折價並加總
                }
                if (tmpProductQuantityGroup.Count() == 4)
                {
                    result += subTotal * 0.8;
                }
                if (tmpProductQuantityGroup.Count() == 3)
                {
                    result += subTotal * 0.9;
                }
                if (tmpProductQuantityGroup.Count() == 2)
                {
                    result += subTotal * 0.95;
                }
                if (tmpProductQuantityGroup.Count() == 1)
                {
                    result += subTotal;
                }
                //扣除已計算的商品數量
                tmpProductQuantityGroup = tmpProductQuantityGroup
                    .Select(group => new KeyValuePair<Product, int>(group.Key, group.Value - 1));
            }
            order.Total = result;
            return order;
        }


    }
}
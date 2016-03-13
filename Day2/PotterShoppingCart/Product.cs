namespace PotterShoppingCart
{
    public class Product
    {
        public Product()
        {
        }

        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Name={Name} Price={Price}";
        }
    }
}
namespace ProductsInPriceRange
{
    using System;

    public class Product : IComparable<Product>
    {
        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            if (this.Price == other.Price && Name != null)
            {
                return this.Name.CompareTo(other.Name);
            }

            return this.Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Price, this.Name);
        }
    }
}

namespace ProductsInRange_Large
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;
    using ProductsInPriceRange;
    using System.Diagnostics;

    class ProductsInRange_Large
    {
        private const int MaxProductsInBag = 500000;
        private const int MaxProductsFromRange = 10000;
        private const int MinPrice = 10; // divide by 100 wil return min decimal 0.1
        private const int MaxPrice = 500000; // divided by 100 will return max decimal 5000
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            var productsBag = FillBag();
            stopWatch.Stop();
            TimeSpan timeForFilling500000InBag = stopWatch.Elapsed;


            decimal minPrice = Convert.ToDecimal(random.Next(MinPrice, MaxPrice)) / 100;
            decimal maxPrice = Convert.ToDecimal(random.Next(MinPrice, MaxPrice)) / 100;
            if(minPrice > maxPrice)
            {
                decimal temp = minPrice;
                minPrice = maxPrice;
                maxPrice = temp;
            }

            var productsInRange = productsBag.Range(
                new Product(null, minPrice),
                true,
                new Product(null, maxPrice),
                true)
                .Take(10000);

            stopWatch.Reset();
            stopWatch.Start();
            Console.WriteLine("Products in range [{0}..{1}]:", minPrice, maxPrice);
            foreach (var product in productsInRange)
            {
                Console.WriteLine(product);
            }
            stopWatch.Stop();
            Console.WriteLine("\nFilling bag with 500000 products finished in {0} secs.", timeForFilling500000InBag);
            Console.WriteLine("Gettign 10000 products from price range finished in {0} secs.", stopWatch.Elapsed);
        }

        private static OrderedBag<Product> FillBag()
        {
            var productsBag = new OrderedBag<Product>();

            for (int i = 0; i < MaxProductsInBag; i++)
            {
                decimal price = Convert.ToDecimal(random.Next(MinPrice, MaxPrice)) / 100;
                string name = "product_" + i;
                var product = new Product(name, price);
                productsBag.Add(product);
            }

            return productsBag;
        }
    }
}

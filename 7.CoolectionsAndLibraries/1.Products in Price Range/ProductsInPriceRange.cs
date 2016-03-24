namespace ProductsInPriceRange
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    class ProductsInPriceRange
    {
        static void Main(string[] args)
        {
            var productsBag = ReadInput();
            decimal[] range = Console.ReadLine()
                .Trim()
                .Split(' ')
                .Select(decimal.Parse)
                .ToArray();

            var productsInRange = productsBag.Range(
                new Product(null, range[0]), 
                true,
                new Product(null, range[1]),
                true)
                .Take(20);

            Console.WriteLine("Productas in range [{0}..{1}]:", range[0], range[1]);
            foreach (var product in productsInRange)
            {
                Console.WriteLine(product);
            }

        }

        private static OrderedBag<Product> ReadInput()
        {
            var productsBag = new OrderedBag<Product>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] inputTokens = Console.ReadLine().Trim().Split(' ');
                var product = new Product(inputTokens[0], decimal.Parse(inputTokens[1]));
                productsBag.Add(product);
            }

            return productsBag;
        }
    }
}

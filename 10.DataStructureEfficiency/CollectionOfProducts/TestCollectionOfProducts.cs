namespace CollectionOfProducts
{
    using System;

    class TestCollectionOfProducts
    {
        static void Main(string[] args)
        {
            var products = new CollectionOfProducts();

            products.Add(1, "C#", "New Editory", 15m);
            products.Add(2, "Java", "New Editory", 12m);
            products.Add(1, "PHP", "Better Editor", 10m);
            products.Add(3, "SQL", "New Editory", 8m);
            products.Add(4, "JS", "Better Editor", 10m);
            products.Add(5, "PHP", "Even Better", 15m);
            products.Add(6, "PHP", "Even Better", 20m);
            products.Add(7, "PHP", "New Editory", 10m);
            products.Add(8, "SQL", "New Editory", 12m);


            Console.WriteLine("Products in price range [10..12]:");
            var productsWithPriceBetween10And12 = products.FindProductsInPriceRange(10m, 12m);
            foreach (var product in productsWithPriceBetween10And12)
            {
                Console.WriteLine("Price {0}: {1}", product.Price, product);
            }

            Console.WriteLine("\nProducts with title PHP:");
            var productsWithTitlePhp = products.FindProductsByTitle("PHP");
            foreach (var product in productsWithTitlePhp)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("\nProducts with title PHP and price 10:");
            var productsWithTitlePhpAndPrice10 = products.FindProductByTitleAndPrice("PHP", 10m);
            foreach (var product in productsWithTitlePhpAndPrice10)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("\nProducts with title PHP and price range [10..15]:");
            var productsWithTitlePhpAndPrice10_15 = products.FindProductsByTitleInPriceRange("PHP", 10m, 15m);
            foreach (var product in productsWithTitlePhpAndPrice10_15)
            {
                Console.WriteLine("Price {0}: {1}", product.Price, product);
            }

            Console.WriteLine("\nProducts from supplier New Editory and price 12:");
            var productsFromNewEditoryWithPrice12 = products.FindProductsBySupplierAndPrice("New Editory", 12m);
            foreach (var product in productsFromNewEditoryWithPrice12)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("\nProducts from supplier New Editory and price range [10..12]:");
            var productsFromNewEditoryWithPrice10_12 = products.FindProductsBySupplierInPriceRange("New Editory", 10m, 12m);
            foreach (var product in productsFromNewEditoryWithPrice10_12)
            {
                Console.WriteLine("Price {0}: {1}", product.Price, product);
            }
        }
    }
}

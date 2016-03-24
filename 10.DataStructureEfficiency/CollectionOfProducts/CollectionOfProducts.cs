namespace CollectionOfProducts
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class CollectionOfProducts
    {
        private Dictionary<int, Product> productsById = new Dictionary<int, Product>();

        private OrderedDictionary<decimal, SortedSet<Product>> productsByPrice = 
            new OrderedDictionary<decimal, SortedSet<Product>>();

        private Dictionary<string, SortedSet<Product>> productsByTitle = 
            new Dictionary<string, SortedSet<Product>>();

        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsByTitleAndPrice =
            new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();

        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsBySupplierAndPrice =
            new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();

        public int Count { get; private set; }
        
        public void Add(int id, string title, string supplier, decimal price)
        {
            var product = new Product(id, title, supplier, price);

            if (this.productsById.ContainsKey(id))
            {
                // Remove the old product from all collections
                this.Remove(id);
            }

            // Add new product in productsById
            this.productsById.Add(id, product);

            // Add new product in productsByPrice 
            this.AddInProductsByPrice(id, price, product);

            // Add new product in productsByTitle
            this.AddInProductsByTitle(id, title, product);

            // Add new product in productsByTitleAndPrice
            this.AddInProductsByTitleAndPrice(id, title, price, product);

            // Add new product in productsBySuplierAndPrice
            this.AddInProductsBySuplierAndPrice(id, supplier, price, product);

            this.Count++;
        }

        public bool Remove(int id)
        {
            if (!this.productsById.ContainsKey(id))
            {
                return false;
            }

            // Remove from producstById
            var product = this.productsById[id];
            this.productsById.Remove(id);

            // Remove from productsByPrice
            this.productsByPrice[product.Price].Remove(product);
            if (this.productsByPrice[product.Price].Count == 0)
            {
                this.productsByPrice.Remove(product.Price);
            }

            // Remove from productsByTitle
            this.productsByTitle[product.Title].Remove(product);
            if(this.productsByTitle[product.Title].Count == 0)
            {
                this.productsByTitle.Remove(product.Title);
            }

            // Remove from productsByTitleAndPrice
            this.productsByTitleAndPrice[product.Title][product.Price].Remove(product);
            if(this.productsByTitleAndPrice[product.Title].Count == 0)
            {
                this.productsByTitleAndPrice.Remove(product.Title);
            }
            else if (this.productsByTitleAndPrice[product.Title][product.Price].Count == 0)
            {
                this.productsByTitleAndPrice[product.Title].Remove(product.Price);
                
            }

            // Remove from productsBySupplierAndPrice
            this.productsBySupplierAndPrice[product.Supplier][product.Price].Remove(product);
            if (this.productsBySupplierAndPrice[product.Supplier].Count == 0)
            {
                this.productsBySupplierAndPrice.Remove(product.Supplier);
            }
            else if (this.productsBySupplierAndPrice[product.Supplier][product.Price].Count == 0)
            {
                this.productsBySupplierAndPrice[product.Supplier].Remove(product.Price);
            }

            this.Count--;

            return true;
        }

        public IEnumerable<Product> FindProductsInPriceRange(decimal startPrice, decimal endPrice)
        {
            var productsInPriceRange = this.productsByPrice.Range(startPrice, true, endPrice, true);
            if (productsInPriceRange == null)
            {
                yield break;
            }

            foreach (var price in productsInPriceRange)
            {
                foreach (var product in price.Value)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FindProductsByTitle(string title)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                return new List<Product>();
            }

            var productsWithTitle = this.productsByTitle[title];

            return productsWithTitle;
        }

        public IEnumerable<Product> FindProductByTitleAndPrice(string title, decimal price)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title) || 
                !this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                return new List<Product>();
            }

            var productsWithTitleAndPrice = this.productsByTitleAndPrice[title][price];

            return productsWithTitleAndPrice;
        }

        public IEnumerable<Product> FindProductsByTitleInPriceRange(string title, decimal startPrice, decimal endPrice)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                yield break;
            }

            var productsWithTitleInPriceRange = 
                this.productsByTitleAndPrice[title].Range(startPrice, true, endPrice, true);
            foreach (var priceRange in productsWithTitleInPriceRange)
            {
                foreach (var product in priceRange.Value)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FindProductsBySupplierAndPrice(string supplier, decimal price)
        {
            if(!this.productsBySupplierAndPrice.ContainsKey(supplier) ||
                !this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                return new List<Product>();
            }

            var productsWithSupplierAndPrice = this.productsBySupplierAndPrice[supplier][price];
            return productsWithSupplierAndPrice;
        }

        public IEnumerable<Product> FindProductsBySupplierInPriceRange(string supplier, decimal startPrice, decimal endPrice)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                yield break;
            }

            var productsFromSupplierInPriceRange = 
                this.productsBySupplierAndPrice[supplier].Range(startPrice, true, endPrice, true);

            foreach (var priceRange in productsFromSupplierInPriceRange)
            {
                foreach (var product in priceRange.Value)
                {
                    yield return product;
                }
            }
        }

        private void AddInProductsByPrice(int id, decimal price, Product product)
        {
            if (!this.productsByPrice.ContainsKey(price))
            {
                var productsSortedById = new SortedSet<Product>();
                this.productsByPrice.Add(price, productsSortedById);
            }

            this.productsByPrice[price].Add(product);

        }

        private void AddInProductsByTitle(int id, string title, Product product)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                var productsSortedById = new SortedSet<Product>();
                this.productsByTitle.Add(title, productsSortedById);
            }

            this.productsByTitle[title].Add(product);

        }

        private void AddInProductsByTitleAndPrice(int id, string title, decimal price, Product product)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                var productsSortedByPrice = new OrderedDictionary<decimal, SortedSet<Product>>();
                this.productsByTitleAndPrice.Add(title, productsSortedByPrice);
            }

            if (!this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                var productsSortedById = new SortedSet<Product>();
                this.productsByTitleAndPrice[title].Add(price, productsSortedById);
            }

            this.productsByTitleAndPrice[title][price].Add(product);
        }

        private void AddInProductsBySuplierAndPrice(int id, string supplier, decimal price, Product product)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                var productsBySupplier = new OrderedDictionary<decimal, SortedSet<Product>>();
                this.productsBySupplierAndPrice.Add(supplier, productsBySupplier);
            }

            if (!this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                var sortedProducts = new SortedSet<Product>();
                this.productsBySupplierAndPrice[supplier].Add(price, sortedProducts);
            }

            this.productsBySupplierAndPrice[supplier][price].Add(product);
        }
    }
}

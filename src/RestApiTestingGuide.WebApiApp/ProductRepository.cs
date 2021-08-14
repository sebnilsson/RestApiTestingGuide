using System;
using System.Collections.Generic;
using System.Linq;
using RestApiTestingGuide.WebApiApp.Models;

namespace RestApiTestingGuide.WebApiApp
{
    public class ProductRepository
    {
        private readonly List<Product> _products = GetDefaultProducts();

        public bool Add(Product product)
        {
            ValidateId(product);

            if (_products.Any(x => x.Id == product.Id))
            {
                return false;
            }

            _products.Add(product);
            return true;
        }

        public bool AddOrUpdate(Product product)
        {
            ValidateId(product);

            var existingProduct = _products.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct == null)
            {
                Add(product);
                return true;
            }

            existingProduct.Update(product);
            return false;
        }

        public bool Delete(int id)
        {
            var removedCount = _products.RemoveAll(x => x.Id == id);

            return removedCount > 0;
        }

        public Product? Get(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public IReadOnlyList<Product> GetAll()
        {
            return _products.OrderBy(x => x.Id).ToList();
        }

        public bool Update(Product updateProduct)
        {
            ValidateId(updateProduct);

            var existingProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Update(updateProduct);
            return true;
        }

        private static void ValidateId(Product product)
        {
            if (product.Id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Product.Id",
                    "Product ID has to be a positive number above zero.");
            }
        }

        private static List<Product> GetDefaultProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Green Chair",
                    Description = "Fits great in your garden."
                },
                new Product
                {
                    Id = 3,
                    Name = "Blue Table",
                    Description = "Matches your kitchen."
                },
                new Product
                {
                    Id = 33,
                    Name = "Red Lamp",
                    Description = "Perfect in your living room."
                },
            };
        }
    }
}

using CarParts.Models;
using CarParts.Helpers;

namespace CarParts.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        private const string FilePath = "Database/products.txt";

        public ProductService()
        {
            try
            {
                _products = FileStorageHelper.ReadFromFile<Product>(FilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                _products = new List<Product>();
            }
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }

        public Product AddProduct(Product product)
        {
            try
            {
                product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
                _products.Add(product);
                FileStorageHelper.WriteToFile(FilePath, _products);

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public bool UpdateProduct(int id, Product updatedProduct)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product == null) return false;

                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Code = updatedProduct.Code;

                FileStorageHelper.WriteToFile(FilePath, _products);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product == null) return false;

                _products.Remove(product);
                FileStorageHelper.WriteToFile(FilePath, _products);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }
    }
}

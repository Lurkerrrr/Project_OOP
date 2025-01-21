using CarParts.Helpers;
using CarParts.Models;

namespace CarParts.Services
{
    public class CategoryService : ICategoryService
    {
        private const string FilePath = "Database/categories.txt"; 
        private readonly List<Category> _categories;

        public CategoryService()
        {
            try
            {
                _categories = FileStorageHelper.ReadFromFile<Category>(FilePath) ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories from file: {ex.Message}");
                _categories = new List<Category>();
            }
        }

        public List<Category> GetAllCategories() => _categories;

        public Category GetCategoryById(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            return category;
        }

        public Category AddCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required.");
            if (category.Name.Length > 50)
                throw new ArgumentException("Category name cannot exceed 50 characters.");
            if (!string.IsNullOrWhiteSpace(category.Description) && category.Description.Length > 200)
                throw new ArgumentException("Category description cannot exceed 200 characters.");

            category.Id = _categories.Count > 0 ? _categories.Max(c => c.Id) + 1 : 1;
            _categories.Add(category);

            try
            {
                FileStorageHelper.WriteToFile(FilePath, _categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving categories to file: {ex.Message}");
                throw new Exception("Failed to save category data.", ex);
            }

            return category;
        }

        public Category UpdateCategory(int id, Category category)
        {
            var existingCategory = GetCategoryById(id);
            if (existingCategory == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required.");
            if (category.Name.Length > 50)
                throw new ArgumentException("Category name cannot exceed 50 characters.");
            if (!string.IsNullOrWhiteSpace(category.Description) && category.Description.Length > 200)
                throw new ArgumentException("Category description cannot exceed 200 characters.");

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            try
            {
                FileStorageHelper.WriteToFile(FilePath, _categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving categories to file: {ex.Message}");
                throw new Exception("Failed to save category data.", ex);
            }

            return existingCategory;
        }

        public bool DeleteCategory(int id)
        {
            var removed = _categories.RemoveAll(c => c.Id == id) > 0;

            if (removed)
            {
                try
                {
                    FileStorageHelper.WriteToFile(FilePath, _categories);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving categories to file: {ex.Message}");
                    throw new Exception("Failed to save category data.", ex);
                }
            }

            return removed;
        }
    }
}
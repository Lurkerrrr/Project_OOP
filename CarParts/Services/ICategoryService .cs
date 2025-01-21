using CarParts.Models;

namespace CarParts.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category AddCategory(Category category);
        Category UpdateCategory(int id, Category category);
        bool DeleteCategory(int id);
    }
}

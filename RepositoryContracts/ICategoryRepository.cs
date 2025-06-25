using Expenditure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.RepositoryContracts
{
    public interface ICategoryRepository
    {
        Category CreateCategory(Category category);
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(int id);
        Category UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}

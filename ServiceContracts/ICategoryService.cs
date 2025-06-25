using Expenditure.Entities;
using Expenditure.ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts
{
    public interface ICategoryService
    {
        CategoryResponse CreateCategory(CategoryAddRequest categoryAddRequest);
        IEnumerable<CategoryResponse> GetCategories();
        CategoryResponse? GetCategoryById(int id);
        CategoryResponse UpdateCategory(CategoryUpdate category);
        void DeleteCategory(int id);
    }
}

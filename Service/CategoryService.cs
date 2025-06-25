using Expenditure.Entities;
using Expenditure.RepositoryContracts;
using Expenditure.ServiceContracts;
using Expenditure.ServiceContracts.DTO;
using Expenditure.ServiceContracts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryResponse CreateCategory(CategoryAddRequest categoryAddRequest)
        {
            return _categoryRepository.CreateCategory(categoryAddRequest.ToCategory())
                .ToCategoryResponse();
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
        }

        public IEnumerable<CategoryResponse> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories.Select(c => c.ToCategoryResponse());
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            return category?.ToCategoryResponse();
        }

        public CategoryResponse UpdateCategory(CategoryUpdate category)
        {
            return _categoryRepository.UpdateCategory(category.ToCategory())
                .ToCategoryResponse();
        }
    }
}

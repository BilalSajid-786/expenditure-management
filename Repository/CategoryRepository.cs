using Expenditure.Entities;
using Expenditure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {

            _context = context;

        }
        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            Category? category = _context.Categories.Find(id);
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public Category? GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

        public Category UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}

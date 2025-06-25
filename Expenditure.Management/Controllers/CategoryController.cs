using Expenditure.Entities;
using Expenditure.ServiceContracts;
using Expenditure.ServiceContracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Expenditure.Management.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_categoryService.GetCategories());
        }

        public IActionResult Create()
        {
            ViewBag.FormAction = "Create";
            ViewBag.Action = "Create";
            return View(new CategoryAddRequest());
        }

        [HttpPost]
        public IActionResult Create(CategoryAddRequest request)
        {
            if(ModelState.IsValid)
            {
                _categoryService.CreateCategory(request);
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
                return NotFound();
            var category = _categoryService.GetCategoryById(id.Value);
            if (category == null)
                return NotFound();
            ViewBag.FormAction = "Update";
            ViewBag.Action = "Update";
            var categoryToUpdate = category?.ToCategoryBase();
            return View(categoryToUpdate);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdate categoryUpdate)
        {
            if(categoryUpdate.CategoryId!= 0 && ModelState.IsValid)
            {
                _categoryService.UpdateCategory(categoryUpdate);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var category = _categoryService.GetCategoryById(id.Value);
            if(category == null)
                return NotFound();
            _categoryService.DeleteCategory(id.Value);
            return RedirectToAction(nameof(Index));
        }
    }
}

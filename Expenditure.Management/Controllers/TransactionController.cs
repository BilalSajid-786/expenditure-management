using Expenditure.ServiceContracts;
using Expenditure.ServiceContracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Expenditure.Management.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;
        public TransactionController(ITransactionService transactionService,
            ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ButtonTitle = "Create Transaction";
            ViewBag.Controller = "Transaction";
            ViewBag.Action = nameof(Create);
            var transactions = await _transactionService.GetAllTransactionAsync();
            return View(transactions);
        }

        public IActionResult Create()
        {
            var categories = _categoryService.GetCategories().ToList();
            categories.Insert(0,new CategoryResponse()
            {
                Title = "Please select category",
                CategoryId = 0
            });
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionAddRequest transactionAddRequest)
        {
            var transaction = await _transactionService.AddTransactionAsync(transactionAddRequest);
            return RedirectToAction(nameof(Index));
        }
    }
}

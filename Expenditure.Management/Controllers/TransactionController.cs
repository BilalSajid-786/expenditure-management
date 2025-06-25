using Expenditure.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Expenditure.Management.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

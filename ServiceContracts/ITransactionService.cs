using Expenditure.ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts
{
    public interface ITransactionService
    {
        Task<TransactionResponse> AddTransactionAsync(TransactionAddRequest? transaction);
        Task<IEnumerable<TransactionResponse>> GetAllTransactionAsync();
        Task<IEnumerable<TransactionResponse>> GetAllTransactionsByCategoryAsync(int categoryId);
        Task<TransactionResponse> UpdateTransactionAsync(TransactionUpdate transaction);
        Task DeleteTransactionAsync(TransactionResponse transaction);
        Task<TransactionResponse> GetTransactionById(int id);
    }
}

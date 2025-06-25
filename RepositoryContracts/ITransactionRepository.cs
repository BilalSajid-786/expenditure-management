using Expenditure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Expenditure.RepositoryContracts
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactionAsync();
        Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryAsync(int categoryId);
        Task<Transaction> UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionById(int id);


    }
}

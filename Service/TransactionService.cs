using Expenditure.RepositoryContracts;
using Expenditure.ServiceContracts;
using Expenditure.ServiceContracts.DTO;
using Expenditure.ServiceContracts.Extensions;
using Expenditure.ServiceContracts.ValidationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Expenditure.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryService _categoryService;
        public TransactionService(ITransactionRepository transactionRepository,
            ICategoryService categoryService)
        {
            _transactionRepository = transactionRepository;
            _categoryService = categoryService;
        }
        public async Task<TransactionResponse> AddTransactionAsync(TransactionAddRequest? transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            if (ModelValidationHelper.TryValidate(transaction))
            {
                if (IsValidCategory(transaction.CategoryId))
                {
                    var response = await _transactionRepository.AddTransactionAsync(transaction.ToTransaction());
                    return response.ToTransactionResponse();
                }
                throw new ArgumentException($"{nameof(transaction.CategoryId)} should be valid");
            }
            return null;
        }

        public async Task DeleteTransactionAsync(TransactionResponse transaction)
        {
            //throw new NotImplementedException();
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            await _transactionRepository.DeleteTransactionAsync(transaction.ToTransaction());
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllTransactionAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionAsync();
            return transactions.Select(t => t.ToTransactionResponse());
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllTransactionsByCategoryAsync(int categoryId)
        {
            if (IsValidCategory(categoryId))
            {
                var transactions = await _transactionRepository.GetAllTransactionsByCategoryAsync(categoryId);
                return transactions.Select(t => t.ToTransactionResponse());
            }
            throw new ArgumentException($"{nameof(TransactionAddRequest.CategoryId)} should be valid");
        }

        public async Task<TransactionResponse> GetTransactionById(int id)
        {
            var transactionResponse = await _transactionRepository.GetTransactionById(id);
            if (transactionResponse == null)
                throw new ArgumentException($"{nameof(transactionResponse.TransactionId)} is not a valid id");
            return transactionResponse.ToTransactionResponse();
        }

        public async Task<TransactionResponse> UpdateTransactionAsync(TransactionUpdate transaction)
        {
            await GetTransactionById(transaction.TransactionId);
            var transactionEntity = transaction.ToTransaction();
            var updatedEntity = await _transactionRepository.UpdateTransactionAsync(transactionEntity);
            return updatedEntity.ToTransactionResponse();
        }

        private bool IsValidCategory(int categoryId)
        {
            return _categoryService.GetCategories()
            .Any(c => c.CategoryId == categoryId);
        }
    }
}

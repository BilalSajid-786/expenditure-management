using Expenditure.Entities;
using Expenditure.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Expenditure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionAsync()
        {
            return await _context.Transactions.Include(t => t.Category).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryAsync(int categoryId)
        {
            var transactions = await _context.Transactions.Where(t => t.CategoryId == categoryId).ToListAsync();
            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == id);
            return transaction;
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            _context.Update(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}

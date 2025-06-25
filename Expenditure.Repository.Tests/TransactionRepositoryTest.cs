using AutoFixture;
using Expenditure.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Expenditure.Repository.Tests
{
    public class TransactionRepositoryTest
    {
        private readonly IFixture _fixture;
        private TransactionRepository _transactionRepository;
        public TransactionRepositoryTest()
        {
            _fixture = new Fixture();
        }

        private ApplicationDbContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"ExpenditureDB-{Guid.NewGuid()}")
                .Options;
            return new ApplicationDbContext( options );
        }


        #region AddTransaction

        [Fact]
        public async Task AddTransaction_ShouldAddTransaction()
        {
            //Arrange
            using var context = GetApplicationDbContext();
            var repo = new TransactionRepository( context );
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .Create();
            _transactionRepository = new TransactionRepository(context);

            //Act
            var actualResult = await _transactionRepository.AddTransactionAsync( transaction );

            //Assert
            context.Transactions?.Count().Should().Be(1);
            transaction.TransactionId.Should().Be( actualResult.TransactionId );
        }


        #endregion

        #region GetTransactions

        [Fact]
        public async Task GetTransactionById_ShouldReturnValidTransaction()
        {
            //Arrange
            using var context = GetApplicationDbContext();
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category).Create();
            var category = _fixture.Build<Category>()
                .Without(c => c.Transactions).Create();
            transaction.CategoryId = category.CategoryId;

            await context.AddAsync(transaction);
            await context.SaveChangesAsync();

            //Act
            _transactionRepository = new TransactionRepository(context);
            var actualResponse = await _transactionRepository.GetTransactionById(transaction.TransactionId);

            //Assert
            actualResponse.Should().NotBeNull();
            actualResponse.TransactionId.Should().Be(transaction.TransactionId);

        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnTransactions()
        {
            //Arrange
            using var context = GetApplicationDbContext();
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .Create();
            var category = _fixture.Build<Category>()
                .Without(c => c.Transactions)
                .Create();
            transaction.CategoryId = category.CategoryId;

            await context.AddAsync(category);
            await context.AddAsync(transaction);
            await context.SaveChangesAsync();

            //Act
            _transactionRepository = new TransactionRepository(context);
            var transactions = await _transactionRepository.GetAllTransactionAsync();

            //Assert
            transactions.Should().HaveCount(1);
            transactions.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllTransactionsByCategory_ShouldReturnTransactions()
        {
            //Arrange
            using var context = GetApplicationDbContext();
            var categories = _fixture.Build<Category>()
                .Without(c => c.Transactions).CreateMany(2);
            var transactions = _fixture.Build<Transaction>()
                .Without(t => t.Category).CreateMany(2);
            foreach ( var transaction in transactions ) 
            {
                transaction.CategoryId = categories.First().CategoryId;
            }
            await context.Categories.AddAsync(categories.First());
            await context.Transactions.AddAsync(transactions.First());
            await context.Transactions.AddAsync(transactions.Last());
            await context.SaveChangesAsync();
            
            _transactionRepository = new TransactionRepository(context);


            //Act
            var actualResult = await _transactionRepository.GetAllTransactionsByCategoryAsync(categories.First().CategoryId);

            //Assert
            actualResult.Count().Should().Be(2);
        }

        #endregion

        #region UpdateTransactions

        [Fact]
        public async Task UpdateTransactionsAsync_ShouldUpdateTransaction_IfValidTransaction()
        {
            //Arrange
            using var context = GetApplicationDbContext();
            var category = _fixture.Build<Category>()
                .Without(c => c.Transactions)
                .Create();
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .Create();
            transaction.CategoryId = category.CategoryId;
            await context.Categories.AddAsync(category);
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            //Act
            _transactionRepository = new TransactionRepository(context);
            transaction.Note = "Updated Notes";
            var actualResult = await _transactionRepository.UpdateTransactionAsync(transaction);

            //Assert
            transaction.Note.Should().Be("Updated Notes");
        }

        #endregion

        #region DeleteTransactions

        [Fact]
        public async Task DeleteTransaction_ShouldDeleteValidTransaction()
        {
            //Arrange
            using var context = GetApplicationDbContext();
            var category = _fixture.Build<Category>()
                .Without(c => c.Transactions)
                .Create();
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .Create();
            await context.Categories.AddAsync(category);
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            _transactionRepository = new TransactionRepository(context);

            //Act
            await _transactionRepository.DeleteTransactionAsync(transaction);

            //Assert
            context.Transactions.Count().Should().Be(0);
        }

        #endregion
    }
}

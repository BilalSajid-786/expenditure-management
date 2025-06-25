using AutoFixture;
using Expenditure.Entities;
using Expenditure.RepositoryContracts;
using Expenditure.ServiceContracts;
using Expenditure.ServiceContracts.DTO;
using Expenditure.ServiceContracts.Extensions;
using FluentAssertions;
using Moq;

namespace Expenditure.Service.Tests
{
    public class TransactionServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly TransactionService _transactionService;
        public TransactionServiceTest()
        {
            _fixture = new Fixture();
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object,
                _categoryServiceMock.Object);
        }

        #region AddTranscation

        [Fact]
        public async Task AddTransaction_ShouldThrowNullArgumentException_IfModelIsNull()
        {
            //Arrange
            TransactionAddRequest? transactionAddRequest = null;

            //Act
            Func<Task> act = async () =>
            {
                 await _transactionService.AddTransactionAsync(transactionAddRequest);
            };

            //Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(true,1)]
        [InlineData(false,0)]
        public async Task AddTransaction_ShouldCheckModelState_IfValid_InValid(bool modelStateValid, int expectedCall)
        {
            //Arrange
            var transaction = _fixture.Build<TransactionAddRequest>()
                .Create();

            if(!modelStateValid)
            {
                transaction.Amount = null;
            }
            else
            {
                //Create a list of valid categories
                var categoryList = _fixture.Build<CategoryResponse>()
                    .CreateMany(3);
                //Mock the categoryservice to return the valid categories
                _categoryServiceMock.Setup(temp => temp.GetCategories())
                    .Returns(categoryList);
                //Add a valid categoryId for transactionAddRequest
                transaction.CategoryId = categoryList.First().CategoryId;
            }

            _transactionRepositoryMock.Setup(temp => temp.AddTransactionAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(transaction.ToTransaction());

            //Act
            var expectedResult = await _transactionService.AddTransactionAsync(transaction);

            //Assert
            _transactionRepositoryMock.Verify(temp => temp.AddTransactionAsync(It.IsAny<Transaction>()), Times.Exactly(expectedCall));
        }

        [Fact]
        public async Task AddTransaction_ShouldThrowArgumentException_IfInValidCategory()
        {
            //Arrange
            //Create a transactionAddRequest instance
            var transaction = _fixture.Build<TransactionAddRequest>().Create();
            //Mock the AddTransactionAsync
            _transactionRepositoryMock.Setup(temp => temp.AddTransactionAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(transaction.ToTransaction());

            //Act
            Func<Task> act = async () =>
            {
                await _transactionService.AddTransactionAsync(transaction);
            };

            //Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        #endregion

        #region GetTransaction

        [Fact]
        public async Task GetAllTransaction_ShouldReturnTransactionList_IfAvailable()
        {
            //Arrange
            var transactions = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .CreateMany(3);
            _transactionRepositoryMock.Setup(temp => temp.GetAllTransactionAsync())
                .ReturnsAsync(transactions);
            var expectedResult = transactions.Select(t => t.ToTransactionResponse());

            //Act
            var actualResult = await _transactionService.GetAllTransactionAsync();

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Fact]
        public async Task GetTransactionsByCategory_ShouldReturnTransactionList_IfValidCategory()
        {
            //Arrange
            var category = _fixture.Build<Category>()
                .Without(c => c.Transactions)
                .CreateMany(1);
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .CreateMany(2);
            transaction.First().CategoryId = category.First().CategoryId;
            transaction.Last().CategoryId = category.First().CategoryId;

            _transactionRepositoryMock.Setup(temp => temp.GetAllTransactionsByCategoryAsync(It.IsAny<int>()))
                .ReturnsAsync(transaction);

            _categoryServiceMock.Setup(temp => temp.GetCategories())
                .Returns(category.Select(c => c.ToCategoryResponse()));
           
            //Act
            var actualResult = await _transactionService.GetAllTransactionsByCategoryAsync(category.First().CategoryId);

            //Assert
            transaction.Should().BeEquivalentTo(actualResult);
        }

        [Fact]
        public async Task GetTransactionById_ShouldReturnTransaction_IfValidId()
        {
            //Arrange
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category).Create();
            _transactionRepositoryMock.Setup(temp => temp.GetTransactionById(It.IsAny<int>()))
                .ReturnsAsync(transaction);

            //Act
            var transactionResponse = await _transactionService.GetTransactionById(transaction.TransactionId);
            var actualResult = transactionResponse.ToTransaction();
            //Assert
            transaction.Should().BeEquivalentTo(actualResult);

        }
        #endregion

        #region DeleteTransaction

        [Fact]
        public async Task DeleteTransaction_ShouldDeleteTransactionId_IfIdValid()
        {
            //Arrange
            var transaction = _fixture.Build<Transaction>()
                .Without(t => t.Category)
                .Create();
            _transactionRepositoryMock.Setup(temp => temp.DeleteTransactionAsync(It.IsAny<Transaction>()));

            //Act
            await _transactionService.DeleteTransactionAsync(transaction.ToTransactionResponse());

            //Assert
            _transactionRepositoryMock.Verify(temp => temp.DeleteTransactionAsync(It.IsAny<Transaction>()), Times.Once);
        }

        #endregion

        #region UpdateTransaction

        [Fact]
        public async Task UpdateTransaction_ShouldUpdateTransaction()
        {
            //Arrange
            var transactionDTO = _fixture.Build<TransactionUpdate>()
                .Create();
            var transactionEntity = transactionDTO.ToTransaction(transactionDTO.TransactionId);
           
            _transactionRepositoryMock.Setup(temp => temp.UpdateTransactionAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(transactionEntity);
            _transactionRepositoryMock.Setup(temp => temp.GetTransactionById(It.IsAny<int>()))
                .ReturnsAsync(transactionEntity);

            //Act
            var expectedResult = await _transactionService.UpdateTransactionAsync(transactionDTO);

            //Assert
            transactionEntity.ToTransactionResponse().Should().BeEquivalentTo(expectedResult);
            _transactionRepositoryMock.Verify(temp => temp.UpdateTransactionAsync(It.IsAny<Transaction>()), Times.Once);
        }

        #endregion
    }
}

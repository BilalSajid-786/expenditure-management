using Expenditure.Entities;
using Expenditure.ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.Extensions
{
    public static class TransactionExtensions
    {
        public static TransactionResponse ToTransactionResponse(this Transaction transaction)
        {
            return new TransactionResponse()
            {
                Amount = transaction.Amount,
                Note = transaction.Note,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                TransactionId = transaction.TransactionId,
            };
        }
    }

    public static class TransactionMapper
    {
        public static Transaction ToTransaction(this TransactionBase transactionBase, int? transactionId = null)
        {
            return new Transaction()
            {
                Amount = transactionBase.Amount ?? 0,
                Note = transactionBase.Note,
                CategoryId = transactionBase.CategoryId,
                Date = transactionBase.Date,
                TransactionId = transactionId ?? 0
            };
        }
    }
}

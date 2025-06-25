using Expenditure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Expenditure.ServiceContracts.DTO
{
    public class TransactionAddRequest : TransactionBase
    {
        public Transaction ToTransaction()
        {
            return new Transaction()
            {
                CategoryId = this.CategoryId,
                Amount = this.Amount ?? 0,
                Note = this.Note,
                Date = this.Date,
            };
        }
    }
}

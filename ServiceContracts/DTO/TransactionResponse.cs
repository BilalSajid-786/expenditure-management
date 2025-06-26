using Expenditure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.DTO
{
    public class TransactionResponse
    {
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public int Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Transaction ToTransaction()
        {
            return new Transaction()
            {
                TransactionId = this.TransactionId,
                Amount = this.Amount,
                Note = this.Note,
                Date = this.Date,
                CategoryId = this.CategoryId,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public int Amount { get; set; }

        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}

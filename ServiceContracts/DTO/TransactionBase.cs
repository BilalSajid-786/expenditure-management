using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.DTO
{
    public class TransactionBase
    {
        [Required]
        public int CategoryId { get; set; }

        public List<CategoryResponse>? Categories { get; set; }

        [Required]
        public int? Amount { get; set; }

        public string? Note { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}

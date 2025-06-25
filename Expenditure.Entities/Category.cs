using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Icon { get; set; }

        [Required]
        public string Type { get; set; } = "Expense";

        public List<Transaction>? Transactions { get; set; }
    }
}

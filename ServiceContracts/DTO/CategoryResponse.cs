using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.DTO
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string Type { get; set; } = "Expense";
        public string? IconTitle { 
            get
            {
                return this.Icon + " " + this.Title; 
            }
        }
        public CategoryBase ToCategoryBase()
        {
            return new CategoryUpdate()
            {
                CategoryId = this.CategoryId,
                Title = this.Title,
                Icon = this.Icon,
                Type = this.Type,
            };
        }
    }
}

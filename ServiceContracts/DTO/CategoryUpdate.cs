using Expenditure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.DTO
{
    public class CategoryUpdate : CategoryBase
    {
        [Required]
        public int CategoryId { get; set; }

        public Category ToCategory()
        {
            return new Category()
            {
                CategoryId = CategoryId,
                Title = this.Title,
                Icon = this.Icon,
                Type = this.Type
            };
        }
    }
}

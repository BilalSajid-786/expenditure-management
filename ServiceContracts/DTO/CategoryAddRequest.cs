using Expenditure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.DTO
{
    public class CategoryAddRequest : CategoryBase
    {
        public Category ToCategory()
        {
            return new Category()
            {
                Title = this.Title,
                Icon = this.Icon,
                Type = this.Type
            };
        }
    }
}

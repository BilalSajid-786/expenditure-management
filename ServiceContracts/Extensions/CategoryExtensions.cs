using Expenditure.Entities;
using Expenditure.ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.Extensions
{
    public static class CategoryExtensions
    {
        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse()
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Type = category.Type,
                Icon = category.Icon
            };
        }
    }
}

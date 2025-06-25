using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.ValidationHelpers
{
    public static class ModelValidationHelper
    {
        public static bool TryValidate(object obj)
        {
            var context = new ValidationContext(obj);
            var result = new List<ValidationResult>();
            bool validationResult = Validator.TryValidateObject(obj, context,result,validateAllProperties:true);
            return validationResult;
        }
    }
}

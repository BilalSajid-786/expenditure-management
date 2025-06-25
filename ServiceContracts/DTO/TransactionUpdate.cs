using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.ServiceContracts.DTO
{
    public class TransactionUpdate : TransactionBase
    {
        [Required]
        public int TransactionId { get; set; }
    }
}

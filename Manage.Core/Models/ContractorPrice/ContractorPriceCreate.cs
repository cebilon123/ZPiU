using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.ContractorPrice
{
    public class ContractorPriceCreate
    {
        [Required]
        public long ProductId { get; set; }
        [Required]
        public long ContractorId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}

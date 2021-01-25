using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.ContractorPrice
{
    public class ContractorPriceDTO
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long ContractorId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}

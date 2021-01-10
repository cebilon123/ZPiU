using Manage.Core.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.Product
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public CategoryDTO Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public string Unit { get; set; }
        public string PkwiuCode { get; set; }
        public string GtuCode { get; set; }
    }
}

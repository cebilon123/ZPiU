using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Models
{
    public class Product: BaseEntity
    {
        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public string Unit { get; set; }
        public string PkwiuCode { get; set; }
        public string GtuCode { get; set; }
    }
}

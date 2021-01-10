using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.Product
{
    public class CreateProductRequest
    {
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Vat { get; set; }
        [Required]
        /// <summary>
        /// Unit. For example: price for one hour, price for one thing etc...
        /// </summary>
        public string Unit { get; set; }
        [Required]
        public string PkwiuCode { get; set; }
        [Required]
        public string GtuCode { get; set; }
    }
}

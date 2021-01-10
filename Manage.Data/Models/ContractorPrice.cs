﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Models
{
    public class ContractorPrice: BaseEntity
    {
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long ContractorId { get; set; }
        public decimal Price { get; set; }
    }
}

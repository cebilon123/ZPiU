using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.Category
{
    public class CategoryCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}

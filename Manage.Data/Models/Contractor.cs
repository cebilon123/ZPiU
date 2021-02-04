using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Models
{
    public class Contractor: BaseEntity
    {
        public long ExternalId { get; set; }
        public string Name { get; set; }
    }
}

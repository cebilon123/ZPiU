using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.Product
{
    public class ProductUpdateResponse: BaseReponse
    {
        public ProductDTO Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Models.ContractorPrice
{
    public class CategoryUpdateResponse: BaseReponse
    {
        public ContractorPriceDTO ContractorPrice { get; set; }
    }
}

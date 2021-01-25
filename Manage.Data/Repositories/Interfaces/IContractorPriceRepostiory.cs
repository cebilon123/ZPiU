using Manage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories.Interfaces
{
    public interface IContractorPriceRepostiory: IBaseRepository<ContractorPrice>
    {
        ContractorPrice GetByContractorIdAndProductId(long contractorId, long productId);
        IEnumerable<ContractorPrice> GetByProductId(long productId);
        IEnumerable<ContractorPrice> GetByContractorId(long contractorId);
    }
}

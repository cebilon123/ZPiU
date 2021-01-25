using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Core.Models;
using Manage.Core.Models.ContractorPrice;

namespace Manage.Core.Services.Interfaces
{
    public interface IContractorPriceService
    {
        Task<BaseReponse> Create(ContractorPriceCreate request);
        Task<BaseReponse> Delete(long contractorId, long productId);
        Task<ICollection<ContractorPriceDTO>> GetList(long contractorId);
        Task<ICollection<ContractorPriceDTO>> GetByProductId(long productId);
        Task<CategoryUpdateResponse> Update(ContractorPriceCreate request, long contractorPriceId);
    }
}

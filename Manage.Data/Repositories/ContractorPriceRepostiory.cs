using Manage.Data.Models;
using Manage.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories
{
    public class ContractorPriceRepostiory: BaseRepository<ContractorPrice>, IContractorPriceRepostiory
    {
        public ContractorPriceRepostiory(BaseContext context): base(context)
        {

        }

        public IEnumerable<ContractorPrice> GetByContractorId(long contractorId) => context.ContractorsPrices
            .Where(c => c.ContractorId == contractorId)
            .Include(ctx => ctx.Product);

        public ContractorPrice GetByProductId(long productId) => context.ContractorsPrices.FirstOrDefault(c => c.ProductId == productId);
    }
}

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

        public IEnumerable<ContractorPrice> GetByProductId(long productId) => context.ContractorsPrices.Where(c => c.ProductId == productId).Include(ctx => ctx.Product).Include(ctx => ctx.Contractor);

        public ContractorPrice GetByContractorIdAndProductId(long contractorId, long productId) => context.ContractorsPrices
            .FirstOrDefault(c => c.ContractorId == contractorId && c.ProductId == productId);
    }
}

using Manage.Data.Models;
using Manage.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories
{
    public class ContractorRepository: BaseRepository<Contractor>, IContractorRepository
    {
        public ContractorRepository(BaseContext context): base(context)
        {

        }
    }
}

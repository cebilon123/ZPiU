using Manage.Core.ExternalProviders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.ExternalProviders.Interfaces
{
    public interface IContractorProvider
    {
        Task<ExternalContractor> GetContractorByNip(string nip);
    }
}

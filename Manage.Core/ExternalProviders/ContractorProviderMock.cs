using Manage.Core.ExternalProviders.Interfaces;
using Manage.Core.ExternalProviders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.ExternalProviders
{
    public class ContractorProviderMock : IContractorProvider
    {
        private readonly List<ExternalContractor> contractors;

        public ContractorProviderMock()
        {
            contractors = new List<ExternalContractor>
            {
                new ExternalContractor
                {
                    Adress = "Testowa 12",
                    Email = "testcontractor@testzpiu.com",
                    Id = 1,
                    Name = "Testowy Contracotr",
                    Nip = "1234567",
                    PhoneNumber = "123-456-789"
                }
            };
        }

        public async Task<ExternalContractor> GetContractorByNip(string nip)
        {
            return await Task.Run(async () =>
                {
                    await Task.Delay(800); //Mock server deley
                    return contractors.FirstOrDefault(c => c.Nip == nip);
                });
        }
    }
}

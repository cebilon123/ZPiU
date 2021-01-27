using Manage.Core.ExternalProviders.Interfaces;
using Manage.Core.ExternalProviders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Manage.Core.ExternalProviders
{
    public class ContractorProvider : IContractorProvider
    {
        private readonly string contractorApiAdress;
        private readonly HttpClient httpClient;
        public ContractorProvider(string contractorApiAdress)
        {
            this.contractorApiAdress = contractorApiAdress;
            httpClient = new HttpClient();
        }

        public async Task<ExternalContractor> GetContractorByNip(string nip)
        {
            using (httpClient)
            {
                var result = await httpClient.GetAsync($"{contractorApiAdress}/{nip}");
                var obtainedContractor = JsonConvert.DeserializeObject<ObtainedContractor>(await result.Content.ReadAsStringAsync());

                if (obtainedContractor is null)
                    return null;

                //Mapping for our internal model
                return new ExternalContractor
                {
                    Adress = obtainedContractor.Miasto,
                    BuildingNumer = obtainedContractor.Nr_Budynku,
                    Email = obtainedContractor.Email,
                    Name = obtainedContractor.Nazwa,
                    Nip = obtainedContractor.Numer_Nip,
                    PhoneNumber = obtainedContractor.Telefon,
                    Id = obtainedContractor.KontrahentId
                };
            }   
        }


        /// <summary>
        /// External object to translate it into our dto's. 
        /// </summary>
        private class ObtainedContractor
        {
            public int KontrahentId { get; set; }
            public string Nazwa { get; set; }
            public string Miasto { get; set; }
            public string Ulica { get; set; }
            public string Nr_Budynku { get; set; }
            public string Kod_Pocztowy { get; set; }
            public string Numer_Nip { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }
        }
    }
}

﻿using AutoMapper;
using Manage.Core.Models;
using Manage.Core.Models.ContractorPrice;
using Manage.Core.Services.Interfaces;
using Manage.Data.Models;
using Manage.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Services
{
    public class ContractorPriceService : IContractorPriceService
    {
        private readonly IContractorPriceRepostiory contractorPriceRepostiory;
        private readonly IMapper mapper;

        public ContractorPriceService(IContractorPriceRepostiory contractorPriceRepostiory, IMapper mapper)
        {
            this.contractorPriceRepostiory = contractorPriceRepostiory;
            this.mapper = mapper;
        }

        public async Task<BaseReponse> Create(ContractorPriceCreate request)
        {
            if (request.ContractorId is 0)
                return new BaseReponse
                {
                    ErrorMessage = "Contractor id can't be 0"
                };
            if (request.ProductId is 0)
                return new BaseReponse
                {
                    ErrorMessage = "Product id can't be 0"
                };

            var insertObject = mapper.Map<ContractorPrice>(request);

            await Task.Run(() =>
            {
                contractorPriceRepostiory.Insert(insertObject);
            });

            if (insertObject.Id is 0)
                return new BaseReponse
                {
                    ErrorMessage = "Error occured while inserting to database"
                };

            return new BaseReponse
            {
                IsSuccessful = true
            };
        }

        public async Task<BaseReponse> Delete(long contractorId, long productId)
        {
            if (contractorId is 0)
                return new BaseReponse
                {
                    ErrorMessage = "Contractor id can't be 0"
                };
            if (productId is 0)
                return new BaseReponse
                {
                    ErrorMessage = "Product id can't be 0"
                };

            var foundContractorPrice = contractorPriceRepostiory.GetByProductId(productId);

            if (foundContractorPrice is null)
                return new BaseReponse
                {
                    ErrorMessage = $"There is no contractor price with productId: {productId}"
                };

            await Task.Run(() =>
            {
                contractorPriceRepostiory.Delete(foundContractorPrice);
            });

            return new BaseReponse
            {
                IsSuccessful = true
            };
        }

        public async Task<ContractorPriceDTO> GetByProductId(long productId) => await Task.Run(() => mapper.Map<ContractorPriceDTO>(contractorPriceRepostiory.GetByProductId(productId)));

        public async Task<ICollection<ContractorPriceDTO>> GetList(long contractorId)
        {
            var contractorPrices = await Task.Run(() =>
            {
                return contractorPriceRepostiory.GetByContractorId(contractorId)
                    .ToList();
            });

            return mapper.Map<List<ContractorPriceDTO>>(contractorPrices);
        }

        public async Task<CategoryUpdateResponse> Update(ContractorPriceCreate request, long contractorPriceId)
        {
            var foundContractorPrice = await Task.Run(() => contractorPriceRepostiory.Get(contractorPriceId));

            if (foundContractorPrice is null)
                return new CategoryUpdateResponse
                {
                    ErrorMessage = $"There is no contractor price with given id: {contractorPriceId}"
                };

            foundContractorPrice.ContractorId = request.ContractorId;
            foundContractorPrice.ProductId = request.ProductId;
            foundContractorPrice.Price = request.Price;

            await Task.Run(() =>
            {
                contractorPriceRepostiory.Update(foundContractorPrice);
            });

            return new CategoryUpdateResponse
            {
                ContractorPrice = mapper.Map<ContractorPriceDTO>(foundContractorPrice),
                IsSuccessful = true
            };
        }
    }
}
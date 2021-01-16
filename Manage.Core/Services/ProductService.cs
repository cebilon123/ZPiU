using AutoMapper;
using Manage.Core.ExternalProviders.Interfaces;
using Manage.Core.Models;
using Manage.Core.Models.Product;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IContractorProvider contractorProvider;
        private readonly IContractorPriceRepostiory contractorPriceRepostiory;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IContractorProvider contractorProvider, IContractorPriceRepostiory contractorPriceRepostiory, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.contractorProvider = contractorProvider;
            this.contractorPriceRepostiory = contractorPriceRepostiory;
            this.mapper = mapper;
        }

        public async Task<BaseReponse> Create(CreateProductRequest request)
        {
            var product = mapper.Map<Product>(request);

            if (categoryRepository.Get(request.CategoryId) is null)
                return new BaseReponse
                {
                    ErrorMessage = "There is no category with given id"
                };

            await Task.Run(() => productRepository.Insert(product));

            if (product.Id is 0)
                return new BaseReponse
                {
                    ErrorMessage = "Error occured while adding to data base"
                };

            return new BaseReponse
            {
                IsSuccessful = true
            };
        }

        public async Task<BaseReponse> Delete(long productId)
        {
            var foundProduct = await Task.Run(() => productRepository.Get(productId));

            if (foundProduct is null)
                return new BaseReponse
                {
                    ErrorMessage = "There is no product with given id"
                };

            await Task.Run(() => productRepository.Delete(foundProduct));

            return new BaseReponse
            {
                IsSuccessful = true
            };
        }

        public async Task<ProductDTO> Get(long id)
        {
            var product = await Task.Run(() => productRepository.GetIncludeCategory(id));

            return mapper.Map<ProductDTO>(product);
        }

        public async Task<ICollection<ProductDTO>> GetAll()
        {
            var products = await Task.Run(() => productRepository.GetListIncludeCategory());

            return mapper.Map<List<ProductDTO>>(products.ToList());
        }

        public async Task<ICollection<ProductDTO>> GetByNip(string nip)
        {
            var foundContractor = await contractorProvider
                .GetContractorByNip(nip);

            if (foundContractor is null)
                return null;

            var contractorPrices = contractorPriceRepostiory
                .GetByContractorId(foundContractor.Id);

            var products = await GetAll();

            products.ToList()
                .ForEach(p =>
                {
                    if(contractorPrices.FirstOrDefault(c => c.ProductId == p.Id) is ContractorPrice contractorPrice)
                    {
                        p.Price = contractorPrice.Price;
                    }
                });

            return products;
        }

        public async Task<ProductUpdateResponse> Update(CreateProductRequest request, long id)
        {
            var foundProduct = await Task.Run(() => productRepository.GetIncludeCategory(id));

            if (foundProduct is null)
                return new ProductUpdateResponse
                {
                    ErrorMessage = "There is no product with given id"
                };

            foundProduct.CategoryId = request.CategoryId;
            foundProduct.Price = request.Price;
            foundProduct.ModifiedAt = DateTime.Now;
            foundProduct.Name = request.Name;
            foundProduct.GtuCode = request.GtuCode;
            foundProduct.PkwiuCode = request.PkwiuCode;
            foundProduct.Unit = request.Unit;
            foundProduct.Vat = request.Vat;

            await Task.Run(() =>
            {
                productRepository.Update(foundProduct);
            });

            return new ProductUpdateResponse
            {
                IsSuccessful = true,
                Product = mapper.Map<ProductDTO>(foundProduct)
            };
        }
    }
}

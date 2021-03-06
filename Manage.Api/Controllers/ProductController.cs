﻿using AutoMapper;
using Manage.Core.ExternalProviders.Interfaces;
using Manage.Core.Models.Category;
using Manage.Core.Models.Product;
using Manage.Core.Services;
using Manage.Core.Services.Interfaces;
using Manage.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IContractorProvider contractorProvider, IContractorRepository contractorRepository, IContractorPriceRepostiory contractorPriceRepostiory, IMapper mapper)
        {
            productService = new ProductService(productRepository, categoryRepository, contractorRepository, contractorProvider,  contractorPriceRepostiory, mapper);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductRequest request)
        {
            var result = await productService.Create(request);

            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await productService.Delete(id);
            if (!result.IsSuccessful)
                return BadRequest(result.ErrorMessage);


            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(long id)
        {
            var result = await productService.Get(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<ProductDTO>>> GetAll()
        {
            return Ok(await productService.GetAll());
        }

        /// <summary>
        /// Returns collection of all products. If nip is given, then it returns collection of products
        /// which prices are replaced by prices set for contractor.
        /// </summary>
        /// <param name="nip">Nip of contractor</param>
        /// <returns></returns>
        [HttpGet("nip/{nip}")]     
        public async Task<ActionResult<ICollection<ProductDTO>>> GetByNip(string nip)
        {
            return Ok(await productService.GetByNip(nip));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductUpdateResponse>> Update([FromBody] CreateProductRequest request, long id)
        {
            var result = await productService.Update(request, id);

            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result.ErrorMessage);
        }
    }
}

using AutoMapper;
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
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            productService = new ProductService(productRepository, categoryRepository, mapper);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductRequest request)
        {
            var result = await productService.Create(request);
            return result.IsSuccessful ? Ok(result) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await productService.Delete(id);
            return result.IsSuccessful ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(long id)
        {
            var result = await productService.Get(id);
            return result is not null ? Ok(result) : NotFound();
        }
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetAll()
        {
            var result = await productService.GetAll();
            return result.Any() ? Ok(result) : NotFound();
        }

        [HttpGet("nip/{nip}")]
        public async Task<ActionResult<ICollection<ProductDTO>>> GetByNip(string nip)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductUpdateResponse>> Update([FromBody] CreateProductRequest request, long id)
        {
            var result = await productService.Update(request, id);
            return result.IsSuccessful ? Ok(result) : BadRequest(result.ErrorMessage);
        }
    }
}

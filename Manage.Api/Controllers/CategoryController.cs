using AutoMapper;
using Manage.Core.Models.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            categoryService = new CategoryService(categoryRepository, mapper);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryCreateResponse>> Create([FromBody] CategoryCreateRequest request)
        {
            var result = await categoryService.Create(request);

            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDeleteResponse>> Remove(long id)
        {
            var result = await categoryService.Delete(id);

            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetResponse>> Get(long id)
        {
            var result = await categoryService.Get(id);

            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetList()
        {
            return Ok(await categoryService.GetList());
        }
    }
}

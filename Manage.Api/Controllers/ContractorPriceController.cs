using AutoMapper;
using Manage.Core.Models.Category;
using Manage.Core.Models.ContractorPrice;
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
    public class ContractorPriceController : ControllerBase
    {
        private readonly IContractorPriceService contractorPriceService;
        public ContractorPriceController(IContractorPriceRepostiory contractorPriceRepostiory, IMapper mapper)
        {
            contractorPriceService = new ContractorPriceService(contractorPriceRepostiory, mapper);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContractorPriceCreate request)
        {
            var result = await contractorPriceService.Create(request);

            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{contractorId}/{productId}")]
        public async Task<ActionResult> Delete(long contractorId, long productId)
        {
            var result = await contractorPriceService.Delete(contractorId, productId);

            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("Contractor/{contractorId}")]
        public async Task<ActionResult<ICollection<ContractorPriceDTO>>> GetList(long contractorId)
        {
            return Ok(await contractorPriceService.GetList(contractorId));
        }

        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<ICollection<ContractorPriceDTO>>> GetOneByProductId(long productId)
        {
            return Ok(await contractorPriceService.GetByProductId(productId));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryUpdateResponse>> UpdateByContractorPriceId([FromBody] ContractorPriceCreate request, long id)
        {
            var result = await contractorPriceService.Update(request, id);

            if (result.IsSuccessful)
                return Ok(result);

            return BadRequest(result.ErrorMessage);
        }
    }
}

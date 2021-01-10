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
            return result.IsSuccessful ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{contractorId}/{productId}")]
        public async Task<ActionResult> Delete(long contractorId, long productId)
        {
            var result = await contractorPriceService.Delete(contractorId, productId);
            return result.IsSuccessful ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpGet("Contractor/{contractorId}")]
        public async Task<ActionResult<ICollection<ContractorPriceDTO>>> GetList(long contractorId)
        {
            var result = await contractorPriceService.GetList(contractorId);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<ContractorPriceDTO>> GetOneByProductId(long productId)
        {
            var result = await contractorPriceService.GetByProductId(productId);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryUpdateResponse>> UpdateByContractorPriceId([FromBody] ContractorPriceCreate request, long id)
        {
            var result = await contractorPriceService.Update(request, id);
            return result.IsSuccessful ? Ok(result) : BadRequest(result.ErrorMessage);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Data.Repositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _taxRepository;

        public TaxController(
            ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        /// <summary>
        /// Retrieves a tax
        /// </summary>
        /// <response code="200">Tax retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{taxId}")]
        public async Task<IActionResult> Get(Guid taxId)
        {
            var tax = await _taxRepository.Get(t => t.TaxId == taxId);

            return Ok(tax);
        }

        /// <summary>
        /// Retrieves all taxes
        /// </summary>
        /// <response code="200">Taxes retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllTaxes()
        {
            var allTaxes = await _taxRepository.GetAll();
            return Ok(allTaxes);
        }

        /// <summary>
        /// Creates a tax
        /// </summary>
        /// <response code="201">Tax created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateTax([FromBody] Tax tax)
        {
            await _taxRepository.Create(tax);
            await _taxRepository.Save();

            return Created("", tax);
        }

        /// <summary>
        /// Updates a tax
        /// </summary>
        /// <response code="204">Tax updated</response>
        /// <response code="409">Tax id's do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{taxId}")]
        public async Task<IActionResult> UpdateTax([FromBody] Tax tax, Guid taxId)
        {
            if (tax.TaxId != taxId)
            {
                return Conflict();
            }
            _taxRepository.Update(tax);
            await _taxRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes a tax
        /// </summary>
        /// <response code="204">Tax Deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{taxId}")]
        public async Task<IActionResult> DeleteTax(Guid taxId)
        {
            if (await _taxRepository.Any(t => t.TaxId == taxId))
            {
                await _taxRepository.Delete(t => t.TaxId == taxId);
                await _taxRepository.Save();
            }
            return NoContent();
        }

        /// <summary>
        /// Assigns tax to a specific order.
        /// </summary>
        /// <response code="200">Tax Assigned</response>
        /// <response code="409">Tax Not Assigned</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("{orderId}")]
        public async Task<IActionResult> AssignTaxToOrder([FromBody] TaxOrderDto taxOrderDto, Guid orderId)
        {
            bool assigned = await _taxRepository.AssignTaxToOrder(taxOrderDto, orderId);

            return assigned ? Ok() : Conflict();
        }
    }
}

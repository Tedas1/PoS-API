using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Data.Repositories;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyProgramController : ControllerBase
    {
        private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;

        public LoyaltyProgramController(
            ILoyaltyProgramRepository loyaltyProgramRepository)
        {
            _loyaltyProgramRepository = loyaltyProgramRepository;
        }

        /// <summary>
        /// Retrieves a loyalty program
        /// </summary>
        /// <response code="200">Loyalty program retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{programId}")]
        public async Task<IActionResult> Get(Guid programId)
        {
            var loyaltyProgram = await _loyaltyProgramRepository.Get(t => t.ProgramId == programId);

            return Ok(loyaltyProgram);
        }

        /// <summary>
        /// Retrieves all loyalty programs
        /// </summary>
        /// <response code="200">Loyalty programs retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllLoyaltyPrograms()
        {
            var allLoyaltyPrograms = await _loyaltyProgramRepository.GetAll();
            return Ok(allLoyaltyPrograms);
        }

        /// <summary>
        /// Creates a loyalty program for user
        /// </summary>
        /// <response code="201">Loyalty program created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateLoyaltyProgram([FromBody] LoyaltyProgram loyaltyProgram)
        {
            await _loyaltyProgramRepository.Create(loyaltyProgram);
            await _loyaltyProgramRepository.Save();

            return Created("", loyaltyProgram);
        }

        /// <summary>
        /// Updates user's loyalty program
        /// </summary>
        /// <response code="204">Loyalty program updated</response>
        /// <response code="409">Loyalty program's ids do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{programId}")]
        public async Task<IActionResult> UpdateLoyaltyProgram([FromBody] LoyaltyProgram loyaltyProgram, Guid programId)
        {
            if (loyaltyProgram.ProgramId != programId)
            {
                return Conflict();
            }
            _loyaltyProgramRepository.Update(loyaltyProgram);
            await _loyaltyProgramRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes a loyalty program for user
        /// </summary>
        /// <response code="204">Loyalty program deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{programId}")]
        public async Task<IActionResult> DeleteTax(Guid programId)
        {
            if (await _loyaltyProgramRepository.Any(t => t.ProgramId == programId))
            {
                await _loyaltyProgramRepository.Delete(t => t.ProgramId == programId);
                await _loyaltyProgramRepository.Save();
            }
            return NoContent();
        }
    }
}

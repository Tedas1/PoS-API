using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipController : ControllerBase
    {
        private readonly ITipRepository _tipRepository;
        private readonly IOrderRepository _orderRepository;
        
        public TipController(ITipRepository tipRepository, IOrderRepository orderRepository)
        {
            _tipRepository = tipRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Retrieves tip
        /// </summary>
        /// <response code="200">tip retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{tipId}")]
        public async Task<IActionResult> Get(Guid tipId)
        {
            var tip = await _tipRepository.Get(x => x.Id == tipId);

            return Ok(tip);
        }

        /// <summary>
        /// Retrieves all tips
        /// </summary>
        /// <response code="200">tips retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllTips()
        {
            var allTips = await _tipRepository.GetAll();
            return Ok(allTips);
        }

        /// <summary>
        /// Creates a tip
        /// </summary>
        /// <response code="201">Tip created</response>
        /// <response code ="400">Order doesn't exist or tip is already created for the specific order</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateTip([FromBody] Tip tip)
        {
            if (!(await _orderRepository.Any(o => o.Id == tip.OrderId)))
            {
                return BadRequest();
            }
            if (await _tipRepository.Any(t => t.OrderId == tip.OrderId))
            {
                return BadRequest();
            }
            await _tipRepository.Create(tip);
            await _tipRepository.Save();

            return Created("", tip);
        }
        /// <summary>
        /// Updates a tip
        /// </summary>
        /// <response code="204">Tip updated</response>
        /// <response code="409">Tip id's do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{tipId}")]
        public async Task<IActionResult> UpdateTip([FromBody] Tip tip, Guid tipId)
        {
            if (tip.Id != tipId)
            {
                return Conflict();
            }
            _tipRepository.Update(tip);
            await _tipRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Deletes a tip
        /// </summary>
        /// <response code="204">Tip Deleted</response>
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{tipId}")]
        public async Task<IActionResult> DeleteTip(Guid tipId)
        {
            if(await _tipRepository.Any(t => t.Id == tipId))
            {
                await _tipRepository.Delete(t => t.Id == tipId);
                await _tipRepository.Save();
            }
            return NoContent();
        }
    }
}

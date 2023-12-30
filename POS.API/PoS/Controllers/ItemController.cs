using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController: ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;

        public ItemController(
            IItemRepository itemRepository,
            IOrderRepository orderRepository)
        {
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allItems = await _itemRepository.GetAll();

            return Ok(allItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Item item)
        {
            await _itemRepository.Create(item);
            await _itemRepository.Save();

            return Created("", item);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem([FromBody] Item item, Guid itemId)
        {
            if (item.Id != itemId) return Conflict();

            _itemRepository.Update(item);
            await _itemRepository.Save();

            return NoContent();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            if (await _itemRepository.Any(x => x.Id == itemId))
            {
                await _itemRepository.Delete(x => x.Id == itemId);
                await _itemRepository.Save();
            }

            return NoContent();
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> AssignItemToOrder([FromBody] Item item, Guid orderId)
        {
            if (await _orderRepository.Any(x => x.Id == orderId))
            {

            }
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController: ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IItemOrderRepository _itemOrderRepository;

        public ItemController(
            IItemRepository itemRepository,
            IOrderRepository orderRepository,
            IItemOrderRepository itemOrderRepository)
        {
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _itemOrderRepository = itemOrderRepository;
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

        /// <summary>
        /// Assigns an item to a specific order.
        /// </summary>
        /// <remarks>Assign method!</remarks>
        /// <response code="200">Item Assigned</response>
        /// <response code="409">Item Not Assigned</response>
        [HttpPost("{orderId}")]
        public async Task<IActionResult> AssignItemToOrder([FromBody] ItemQuantityDto itemQuantity, Guid orderId)
        {
            bool assigned = await _itemRepository.AssignItemToOrder(itemQuantity, orderId);

            return assigned ? Ok() : Conflict();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderItems(Guid orderId)
        {
            //if (await _orderRepository.Any(x => x.Id == orderId))
            //{
            //    ICollection<Item> items = await _orderRepository.GetOrderItems(orderId);
            //    return Ok(items);
            //}
            return NotFound();
        }

        [HttpPut("{orderId}/{itemId}")]
        public async Task<IActionResult> UpdateOrderItem(Guid orderId, Guid itemId)
        {
            return NotFound();
        }

        [HttpDelete("{orderId}/{itemId}")]
        public async Task<IActionResult> DeleteItemFromOrder(Guid orderId, Guid itemId)
        {
            return NotFound();
        }
    }
}

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

        /// <summary>
        /// Retrieves all items
        /// </summary>
        /// <response code="200">All items retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allItems = await _itemRepository.GetAll();

            return Ok(allItems);
        }

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <response code="201">Created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Item item)
        {
            await _itemRepository.Create(item);
            await _itemRepository.Save();

            return Created("", item);
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <response code="204">No Content</response>
        /// <response code="409">Item Id's do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem([FromBody] Item item, Guid itemId)
        {
            if (item.Id != itemId) return Conflict();

            _itemRepository.Update(item);
            await _itemRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        /// <response code="200">Item Assigned</response>
        /// <response code="409">Item Not Assigned</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("{orderId}")]
        public async Task<IActionResult> AssignItemToOrder([FromBody] ItemQuantityDto itemQuantity, Guid orderId)
        {
            if (!await _orderRepository.Any(x => x.Id == orderId)) return Conflict();

            bool assigned = await _itemRepository.AssignItemToOrder(itemQuantity, orderId);

            return assigned ? Ok() : Conflict();
        }


        /// <summary>
        /// Retrieves all items assigned to an order
        /// </summary>
        /// <response code="200">Items Retrieved</response>
        /// <response code="404">Not Found any Items</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderItems(Guid orderId)
        {
            if (await _orderRepository.Any(x => x.Id == orderId))
            {
                var orderItems = await _itemOrderRepository.GetMany(x => x.OrderId == orderId);

                List<Item> items = new List<Item>();
                foreach(var orderItem in orderItems)
                {
                    var item = await _itemRepository.Get(x => x.Id == orderItem.ItemId);
                    if (item != null)
                    {
                        item.Stock = orderItem.Quantity;
                        items.Add(item);
                    }
                }
                return Ok(items);
            }

            return NotFound();
        }


        /// <summary>
        /// Deletes an item assigned to an order
        /// </summary>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{orderId}/{itemId}")]
        public async Task<IActionResult> DeleteItemFromOrder(Guid orderId, Guid itemId)
        {
            if (await _itemOrderRepository.Any(x => x.OrderId == orderId && x.ItemId == itemId))
            {
                await _itemOrderRepository.Delete(x => x.OrderId == orderId && x.ItemId == itemId);
                await _itemOrderRepository.Save();
            }

            return NoContent();
        }
    }
}

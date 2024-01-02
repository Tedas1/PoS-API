using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <response code="201">Created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderRepository.Create(order);
            await _orderRepository.Save();

            return Created("", new OrderDto()
            { 
                Id = order.Id,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId
            });
        }

        /// <summary>
        /// Retrieves all orders
        /// </summary>
        /// <response code="200">Retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allOrders = await _orderRepository.GetAll();

            return Ok(allOrders);
        }

        /// <summary>
        /// Retrieves an order
        /// </summary>
        /// <response code="200">Retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var order = await _orderRepository.Get(x => x.Id == orderId);

            return Ok(order);
        }

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <response code="204">Updates</response>
        /// <response code="409">Order id's do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order, Guid orderId)
        {
            if (order.Id != orderId) return Conflict();

            _orderRepository.Update(order);
            await _orderRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            if (await _orderRepository.Any(x => x.Id == orderId))
            {
                await _orderRepository.Delete(x => x.Id == orderId);
                await _orderRepository.Save();
            }

            return NoContent();
        }

        /// <summary>
        /// Initiates refund process
        /// </summary>
        /// <response code="202">Accepted</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{orderId}/refund")]
        public async Task<IActionResult> RefundOrder(Guid orderId)
        {
            if (await _orderRepository.Any(x => x.Id == orderId))
            {
                Order order = await _orderRepository.Get(x => x.Id == orderId) ?? new Order();
                order.Status = Enums.OrderStatus.Refund;
                _orderRepository.Update(order);
                await _orderRepository.Save();

                return Accepted();
            }

            return NoContent();
        }
    }
}

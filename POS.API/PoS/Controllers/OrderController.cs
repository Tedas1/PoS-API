using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Data.Repositories;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allOrders = await _orderRepository.GetAll();

            return Ok(allOrders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var order = await _orderRepository.Get(x => x.Id == orderId);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderRepository.Create(order);
            await _orderRepository.Save();

            return Created("", order);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateItem([FromBody] Order order, Guid orderId)
        {
            if (order.Id != orderId) return Conflict();

            _orderRepository.Update(order);
            await _orderRepository.Save();

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteItem(Guid orderId)
        {
            if (await _orderRepository.Any(x => x.Id == orderId))
            {
                await _orderRepository.Delete(x => x.Id == orderId);
                await _orderRepository.Save();
            }

            return NoContent();
        }
    }
}

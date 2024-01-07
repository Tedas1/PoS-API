using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Data.Repositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
       
        public PaymentController(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Creates a payment
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="400">Order does not exist or the order is paid</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (!(await _orderRepository.Any(o => o.Id == payment.OrderId)))
            {
                return BadRequest();
            }
            if (await _paymentRepository.Any(p => p.OrderId == payment.OrderId))
            {
                return BadRequest();
            }

            await _paymentRepository.Create(payment);
            await _paymentRepository.Save();

            return Created("", payment);
        }

        /// <summary>
        /// Retrieves all payments
        /// </summary>
        /// <response code="200">Retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allPayments = await _paymentRepository.GetAll();

            return Ok(allPayments);
        }

        /// <summary>
        /// Retrieves a payment
        /// </summary>
        /// <response code="200">Retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{paymnetId}")]
        public async Task<IActionResult> GetPayment(Guid paymnetId)
        {
            var payment = await _paymentRepository.Get(p => p.PaymentId == paymnetId);

            return Ok(payment);
        }

        /// <summary>
        /// Updates a payment
        /// </summary>
        /// <response code="204">Updates</response>
        /// <response code="409">Payment id's do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{paymentId}")]
        public async Task<IActionResult> UpdatePayment([FromBody] Payment payment, Guid paymentId)
        {
            if (payment.PaymentId != paymentId) return Conflict();

            _paymentRepository.Update(payment);
            await _paymentRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes a payment
        /// </summary>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{paymentId}")]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            if (await _paymentRepository.Any(p => p.PaymentId == paymentId))
            {
                await _paymentRepository.Delete(x => x.PaymentId == paymentId);
                await _paymentRepository.Save();
            }
            return NoContent();
        }
    }
}

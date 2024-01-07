using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;
using PoS.Enums;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;

        public ReservationController(
            IReservationRepository reservationRepository,
            IUserRepository userRepository
            )
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Retrieves all reservations by date
        /// </summary>
        /// <response code="200">Reservations retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var reservations = await _reservationRepository.GetMany(x => x.Date == date);

            return Ok(reservations);
        }

        /// <summary>
        /// Retrieves all reservations by employee
        /// </summary>
        /// <response code="200">Reservations retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId)
        {
            var reservations = await _reservationRepository.GetMany(x => x.EmployeeId == employeeId);

            return Ok(reservations);
        }

        /// <summary>
        /// Retrieves reservation
        /// </summary>
        /// <response code="200">Reservation retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reservations = await _reservationRepository.Get(x => x.Id == id);

            return Ok(reservations);
        }

        /// <summary>
        /// Creates a reservation
        /// </summary>
        /// <response code="201">Reservation created</response>
        /// <response code="400">Incorrect employee role</response>
        /// <response code="409">Unavailable time slot</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody]Reservation reservation)
        {
            // User referenced by EmployeeId is a customer
            if (await _userRepository.Any(x =>
                x.Id == reservation.EmployeeId
                && x.Role == UserRole.Customer))
            {
                return BadRequest();
            }

            // Time slot is unavailable
            if (await _reservationRepository.Any(x =>
                x.Date == reservation.Date
                && x.TimeSlot == reservation.TimeSlot
                && x.EmployeeId == reservation.EmployeeId))
            {
                return Conflict();
            }

            await _reservationRepository.Create(reservation);
            await _reservationRepository.Save();

            return Created("", reservation);
        }

        /// <summary>
        /// Deletes a reservation
        /// </summary>
        /// <response code="204">Reservation deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteUser(Guid reservationId)
        {
            if (await _reservationRepository.Any(x => x.Id == reservationId))
            {
                await _reservationRepository.Delete(x => x.Id == reservationId);
                await _reservationRepository.Save();
            }

            return NoContent();
        }
    }
}

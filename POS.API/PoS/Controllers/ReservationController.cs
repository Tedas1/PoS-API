using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Data.Repositories;
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
        /// Retrieves all free slots by date and employee
        /// </summary>
        /// <response code="200">Free slots retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("slots/{date}/{employeeId}")]
        public async Task<IActionResult> GetFreeSlots(Guid employeeId, DateTime date)
        {
            var reservations = await _reservationRepository.GetMany(x =>
                x.Date == date
                && x.EmployeeId == employeeId
                && x.Status != ReservationStatus.Cancelled);

            var reservedSlots =
                from reservation in reservations
                select reservation.TimeSlot;

            // working hours from 9 to 21
            var allSlots = Enumerable.Range(9, 12);

            var freeSlots = allSlots.Except(reservedSlots);

            return Ok(freeSlots);
        }


        /// <summary>
        /// Retrieves all pending reservations
        /// </summary>
        /// <response code="200">Reservations retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllPendingReservations()
        {
            var allReservations = await _reservationRepository.GetMany(x => x.Status == ReservationStatus.Pending);
            
            return Ok(allReservations);
        }

        /// <summary>
        /// Retrieves reservation
        /// </summary>
        /// <response code="200">Reservation retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reservation = await _reservationRepository.Get(x => x.Id == id);

            return Ok(reservation);
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
                && x.EmployeeId == reservation.EmployeeId
                && x.Status != ReservationStatus.Cancelled))
            {
                return Conflict();
            }

            await _reservationRepository.Create(reservation);
            await _reservationRepository.Save();

            return Created("", reservation);
        }

        /// <summary>
        /// Updates a reservation
        /// </summary>
        /// <response code="204">Reservation updated</response>
        /// <response code="400">Incorrect reservation id or reservation is cancelled</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{reservationId}/{status}")]
        public async Task<IActionResult> UpdateReservation(Guid reservationId, ReservationStatus status)
        {
            var reservation = await _reservationRepository.Get(x => x.Id == reservationId);

            if (reservation == null || reservation.Status == ReservationStatus.Cancelled)
            {
                return BadRequest();
            }

            reservation.Status = status;

            await _reservationRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes a reservation
        /// </summary>
        /// <response code="204">Reservation deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation(Guid reservationId)
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

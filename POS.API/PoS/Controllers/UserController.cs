using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;            
        }

        /// <summary>
        /// Retrieves user
        /// </summary>
        /// <response code="200">User retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _userRepository.Get(x => x.Id == userId);

            return Ok(user);
        }

        /// <summary>
        /// Retrieves all users
        /// </summary>
        /// <response code="200">Users retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await _userRepository.GetAll();
            return Ok(allUsers);
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <response code="201">Users created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userRepository.Create(user);
            await _userRepository.Save();
            
            return Created("", user);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <response code="204">User updated</response>
        /// <response code="409">User id's do not match</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, Guid userId)
        {
            if (user.Id != userId) return Conflict();

            _userRepository.Update(user);
            await _userRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <response code="204">User Deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if (await _userRepository.Any(x => x.Id == userId))
            {
                await _userRepository.Delete(x => x.Id == userId);
                await _userRepository.Save();
            }

            return NoContent();
        }
    }
}

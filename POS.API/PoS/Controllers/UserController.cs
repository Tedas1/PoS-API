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

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _userRepository.Get(x => x.Id == userId);

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await _userRepository.GetAll();
            return Ok(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userRepository.Create(user);
            await _userRepository.Save();
            
            return Created("", user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, Guid userId)
        {
            if (user.Id != userId) return Conflict();

            _userRepository.Update(user);
            await _userRepository.Save();

            return NoContent();
        }

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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.Abstractions.Repositories.EntityRepositories;

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
    }
}

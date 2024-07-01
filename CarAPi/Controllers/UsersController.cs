using CarAPi.Entities;
using CarAPi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarAPi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            var userToRegister = await _userService.Register(user);
            if(userToRegister == null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok(userToRegister);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(User user)
        {
            var result = await _userService.Login(user);

            if(!result.Success)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(result.Token);
        }
    }
}

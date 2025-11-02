using BestPracticesEFIdentity.Core.Dtos;
using BestPracticesEFIdentity.Service.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticesEFIdentity.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto, CancellationToken cancellation)
        {
            var res = await _userService.CreateUserAsync(createUserDto, cancellation);
            return Ok(res);
        }
        [HttpPost] 
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellation)
        {
            var res = await _userService.LoginAsync(loginDto, cancellation);
            return Ok(res);
        }
        

    }
}

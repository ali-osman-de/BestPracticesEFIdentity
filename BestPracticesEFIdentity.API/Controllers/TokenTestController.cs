using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticesEFIdentity.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [ApiController]
    public class TokenTestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            var res = "Auth Başarılı bir test oldu!";
            return Ok(res);
        }
    }
}

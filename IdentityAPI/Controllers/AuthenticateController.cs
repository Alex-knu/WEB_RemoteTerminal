using IdentityApi.Core.Interfaces.Services;
using IdentityApi.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {private readonly IAuthenticateSevice _authenticateSevice;

        public AuthenticateController(IAuthenticateSevice authenticateSevice)
        {
            _authenticateSevice = authenticateSevice;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok(await _authenticateSevice.Login(model));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            return Ok(await _authenticateSevice.Register(model));
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            await _authenticateSevice.RegisterAdmin(model);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
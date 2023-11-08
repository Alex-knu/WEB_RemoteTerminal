using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SystemUserToMachineUserController : ControllerBase
    {
        private readonly ISystemUserToMachineUserService _systemUserToMachineUserService;

        public SystemUserToMachineUserController(ISystemUserToMachineUserService systemUserToMachineUserService)
        {
            _systemUserToMachineUserService = systemUserToMachineUserService;
        }

        [HttpGet("collection", Name = "GetMachineUsers")]
        public async Task<IActionResult> GetMachineUsers()
        {
            return Ok(await _systemUserToMachineUserService.GetMachineUsers(HttpContext));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MachineUserDTO query)
        {
            return Ok(await _systemUserToMachineUserService.Create(HttpContext, query));
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid query)
        {
            return Ok(await _systemUserToMachineUserService.Delete(query));
        }
    }
}

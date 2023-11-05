using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemUserToMachineUserController : ControllerBase
    {
        private readonly ISystemUserToMachineUserService _systemUserToMachineUserService;

        public SystemUserToMachineUserController(ISystemUserToMachineUserService systemUserToMachineUserService)
        {
            _systemUserToMachineUserService = systemUserToMachineUserService;
        }

        [HttpGet("{guid}", Name = "GetMachineUsers")]
        public async Task<IActionResult> GetMachineUsers(Guid query)
        {
            return Ok(await _systemUserToMachineUserService.GetMachineUsers(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SystemUserToMachineUserDTO query)
        {
            return Ok(await _systemUserToMachineUserService.Create(query));
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid query)
        {
            return Ok(await _systemUserToMachineUserService.Delete(query));
        }
    }
}

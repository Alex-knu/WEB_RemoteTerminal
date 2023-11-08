using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MachineUserController : ControllerBase
    {
        private readonly IMachineUserService _machineUserService;
        
        public MachineUserController(IMachineUserService machineUserService)
        {
            _machineUserService = machineUserService;
        }

        [HttpGet("{guid}", Name = "GetMachineUser")]
        public async Task<IActionResult> Get(Guid query)
        {
            return Ok(await _machineUserService.Get(query));
        }

        [HttpGet("collection/{guid}", Name = "GetMachineAllMachineUsers")]
        public async Task<IActionResult> GetMachineAllMachineUsers(Guid query)
        {
            return Ok(await _machineUserService.GetMachineAllMachineUsers(query));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MachineUserDTO query)
        {
            return Ok(await _machineUserService.Create(query));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MachineUserDTO query)
        {
            return Ok(await _machineUserService.Update(query));
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid query)
        {
            return Ok(await _machineUserService.Delete(query));
        }
    }
}

using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;
        
        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }
        
        [HttpGet("{guid}", Name = "Get")]
        public async Task<IActionResult> Get(Guid query)
        {
            return Ok(await _machineService.Get(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MachineDTO query)
        {
            return Ok(await _machineService.Create(query));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MachineDTO query)
        {
            return Ok(await _machineService.Update(query));
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid query)
        {
            return Ok(await _machineService.Delete(query));
        }
    }
}

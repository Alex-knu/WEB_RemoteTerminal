using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommandHistoryController : ControllerBase
    {        
        private readonly ICommandHistoryService _commandHistoryService;

        public CommandHistoryController(ICommandHistoryService commandHistoryService)
        {
            _commandHistoryService = commandHistoryService;
        }

        [HttpGet("collection/{guid}", Name = "GetMachineUserCommands")]
        public async Task<IActionResult> GetMachineUserCommands(Guid guid)
        {
            return Ok(await _commandHistoryService.GetMachineUserCommands(guid));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommandHistoryDTO query)
        {
            return Ok(await _commandHistoryService.Create(query));
        }
    }
}

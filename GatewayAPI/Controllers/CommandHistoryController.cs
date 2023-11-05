using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandHistoryController : ControllerBase
    {        
        private readonly ICommandHistoryService _commandHistoryService;

        public CommandHistoryController(ICommandHistoryService commandHistoryService)
        {
            _commandHistoryService = commandHistoryService;
        }

        [HttpGet("{guid}", Name = "GetMachineUserCommands")]
        public async Task<IActionResult> GetMachineUserCommands(Guid query)
        {
            return Ok(await _commandHistoryService.GetMachineUserCommands(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommandHistoryDTO query)
        {
            return Ok(await _commandHistoryService.Create(query));
        }
    }
}

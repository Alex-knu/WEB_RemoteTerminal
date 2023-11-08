using DataManagerAPI.Core.Entities;
using DataManagerAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandHistoryController : ControllerBase
    {        
        private readonly ICommandHistoryRepository _CommandHistoryRepository;

        public CommandHistoryController(ICommandHistoryRepository commandHistoryRepository)
        {
            _CommandHistoryRepository = commandHistoryRepository;
        }

        [HttpGet("collection/{guid}", Name = "GetMachineUserCommands")]
        public async Task<IActionResult> GetMachineUserCommands(Guid guid)
        {
            IEnumerable<CommandHistory> commandHistory = await _CommandHistoryRepository.GetCommandHistoryOfMachineUserAsync(guid);

            if (commandHistory.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(commandHistory.OrderByDescending(c => c.Time));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommandHistory commandHistory)
        {
            if (commandHistory == null)
            {
                return BadRequest();
            }
            try
            {
                await _CommandHistoryRepository.AddAsync(commandHistory);
            }
            catch
            {
                return BadRequest("Error occured. Possibly machineUserId is not valid.");
            }
            return new ObjectResult(commandHistory);
        }
    }
}

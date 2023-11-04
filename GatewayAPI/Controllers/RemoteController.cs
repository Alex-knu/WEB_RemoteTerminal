using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemoteController : ControllerBase
    {
        private readonly ILogger<RemoteController> _logger;
        private readonly IRemoteService _remoteService;

        public RemoteController(ILogger<RemoteController> logger, IRemoteService remoteService)
        {
            _logger = logger;
            _remoteService = remoteService;
        }

        [HttpPost]
        public async Task<IActionResult> RunCommandAsync(RunCommandDTO query)
        {
            return Ok(await _remoteService.RunCommandAsync(query));
        }
    }
}
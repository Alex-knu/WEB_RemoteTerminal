using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Services
{
    internal class RemoteService : IRemoteService
    {
        private readonly HttpClient _client;
        private readonly HttpClient _dataClient;
        private readonly IRouteService _routeService;

        public RemoteService(IHttpClientFactory httpClientFactory, IRouteService routeService)
        {
            _dataClient = httpClientFactory.CreateClient("DataServer");
            _client = httpClientFactory.CreateClient("RemoteServer");
            _routeService = routeService;
        }

        public async Task<RunCommandResponceDTO> RunCommandAsync(RunCommandDTO query)
        {
            var history = new CommandHistoryDTO()
            {
                Id = Guid.NewGuid(),
                MachineUserId = query.MachineUserId,
                Time = DateTime.Now,
                Command = query.Command
            };

            _routeService.PostAsJsonAsync<CommandHistoryDTO, string>(_dataClient, "CommandHistory", history);

            return new RunCommandResponceDTO(){ Result =  await _routeService.PostAsJsonAsync<RunCommandDTO, string>(_client, "connect", query) };
        }
    }
}
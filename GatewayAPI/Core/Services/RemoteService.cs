using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Services
{
    internal class RemoteService : IRemoteService
    {
        private readonly HttpClient _client;
        private readonly IRouteService _routeService;

        public RemoteService(IHttpClientFactory httpClientFactory, IRouteService routeService)
        {
            _client = httpClientFactory.CreateClient("RemoteServer");
            _routeService = routeService;
        }

        public async Task<string> RunCommandAsync(RunCommandDTO query)
        {
            return await _routeService.PostAsJsonAsync<RunCommandDTO, string>(_client, "connect", query);
        }
    }
}
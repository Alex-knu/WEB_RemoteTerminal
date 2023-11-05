using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Core.Services
{
    public class CommandHistoryService : ICommandHistoryService
    {
        private readonly HttpClient _client;
        private readonly IRouteService _routeService;

        public CommandHistoryService(IHttpClientFactory httpClientFactory, IRouteService routeService)
        {
            _client = httpClientFactory.CreateClient("DataServer");
            _routeService = routeService;
        }

        public async Task<CommandHistoryDTO> Create(CommandHistoryDTO query)
        {
            return await _routeService.PostAsJsonAsync<CommandHistoryDTO, CommandHistoryDTO>(_client, "CommandHistory", query);
        }

        public async Task<IEnumerable<CommandHistoryDTO>> GetMachineUserCommands(Guid query)
        {
            return await _routeService.GetByIdAsync<IEnumerable<CommandHistoryDTO>, Guid>(_client, "CommandHistory", query);
        }
    }
}
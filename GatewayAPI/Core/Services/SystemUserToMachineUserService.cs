using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Services
{
    public class SystemUserToMachineUserService : ISystemUserToMachineUserService
    {
        private readonly HttpClient _client;
        private readonly IRouteService _routeService;

        public SystemUserToMachineUserService(IHttpClientFactory httpClientFactory, IRouteService routeService)
        {
            _client = httpClientFactory.CreateClient("DataServer");
            _routeService = routeService;
        }

        public async Task<SystemUserToMachineUserDTO> Create(SystemUserToMachineUserDTO query)
        {
            return await _routeService.PostAsJsonAsync<SystemUserToMachineUserDTO, SystemUserToMachineUserDTO>(_client, "MachineUser", query);
        }

        public async Task<SystemUserToMachineUserDTO> Delete(Guid query)
        {
            return await _routeService.DeleteAsJsonAsync<SystemUserToMachineUserDTO, Guid>(_client, "MachineUser", query);
        }

        public async Task<IEnumerable<SystemUserToMachineUserDTO>> GetMachineUsers(Guid query)
        {
            return await _routeService.GetListByIdAsync<IEnumerable<SystemUserToMachineUserDTO>, Guid>(_client, "MachineUser", query);
        }
    }
}
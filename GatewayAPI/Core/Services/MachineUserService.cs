using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Core.Services
{
    public class MachineUserService : IMachineUserService
    {
        private readonly HttpClient _client;
        private readonly IRouteService _routeService;

        public MachineUserService(IHttpClientFactory httpClientFactory, IRouteService routeService)
        {
            _client = httpClientFactory.CreateClient("DataServer");
            _routeService = routeService;
        }

        public async Task<MachineUserDTO> Create(MachineUserDTO query)
        {
            return await _routeService.PostAsJsonAsync<MachineUserDTO, MachineUserDTO>(_client, "MachineUser", query);
        }

        public async Task<MachineUserDTO> Delete(Guid query)
        {
            return await _routeService.DeleteAsJsonAsync<MachineUserDTO, Guid>(_client, "MachineUser", query);
        }

        public async Task<MachineUserDTO> Get(Guid query)
        {
            return await _routeService.GetByIdAsync<MachineUserDTO, Guid>(_client, "MachineUser", query);
        }

        public async Task<IEnumerable<MachineUserDTO>> GetMachineAllMachineUsers(Guid query)
        {
            return await _routeService.GetListByIdAsync<IEnumerable<MachineUserDTO>, Guid>(_client, "MachineUser", query);
        }

        public async Task<MachineUserDTO> Update(MachineUserDTO query)
        {
            return await _routeService.PutAsJsonAsync<MachineUserDTO, MachineUserDTO>(_client, "MachineUser", query);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Core.Services
{
    public class MachineService : IMachineService
    {
        private readonly HttpClient _client;
        private readonly IRouteService _routeService;

        public MachineService(IHttpClientFactory httpClientFactory, IRouteService routeService)
        {
            _client = httpClientFactory.CreateClient("DataServer");
            _routeService = routeService;
        }

        public async Task<MachineDTO> Create(MachineDTO query)
        {
            return await _routeService.PostAsJsonAsync<MachineDTO, MachineDTO>(_client, "Machine", query);
        }

        public async Task<MachineDTO> Delete(Guid query)
        {
            return await _routeService.DeleteAsJsonAsync<MachineDTO, Guid>(_client, "Machine", query);
        }

        public async Task<MachineDTO> Get(Guid query)
        {
            return await _routeService.GetByIdAsync<MachineDTO, Guid>(_client, "Machine", query);
        }

        public async Task<MachineDTO> Update(MachineDTO query)
        {
            return await _routeService.PutAsJsonAsync<MachineDTO, MachineDTO>(_client, "Machine", query);
        }
    }
}
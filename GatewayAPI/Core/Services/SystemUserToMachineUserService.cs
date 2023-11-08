using GatewayAPI.Core.Interfaces;
using GatewayAPI.Core.Models.DTO;
using GatewayAPI.Extentions.Extentions;

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

        public async Task<SystemUserToMachineUserDTO> Create(HttpContext httpContext, MachineUserDTO query)
        { 
            var request = new SystemUserToMachineUserDTO()
            {
                Id = Guid.NewGuid(),
                SystemUserId = JwtUtils.GetUserInfo(httpContext).UserId,
                MachineUser = query,
                MachineUserId = query.Id
            };

            return await _routeService.PostAsJsonAsync<SystemUserToMachineUserDTO, SystemUserToMachineUserDTO>(_client, "SystemUserToMachineUser", request);
        }

        public async Task<SystemUserToMachineUserDTO> Delete(Guid query)
        {
            return await _routeService.DeleteAsJsonAsync<SystemUserToMachineUserDTO, Guid>(_client, "SystemUserToMachineUser", query);
        }

        public async Task<IEnumerable<MachineUserDTO?>> GetMachineUsers(HttpContext httpContext)
        {
            return (await _routeService.GetListByIdAsync<IEnumerable<SystemUserToMachineUserDTO>, Guid>(_client, "SystemUserToMachineUser", JwtUtils.GetUserInfo(httpContext).UserId)).Select(e => e.MachineUser);
        }
    }
}
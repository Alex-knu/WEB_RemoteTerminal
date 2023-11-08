using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Interfaces
{
    public interface ISystemUserToMachineUserService
    {
        Task<IEnumerable<MachineUserDTO?>> GetMachineUsers(HttpContext httpContext);
        Task<SystemUserToMachineUserDTO> Create(HttpContext httpContext, MachineUserDTO query);
        Task<SystemUserToMachineUserDTO> Delete(Guid query);
    }
}
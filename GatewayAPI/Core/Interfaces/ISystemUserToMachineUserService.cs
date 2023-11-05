using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Core.Interfaces
{
    public interface ISystemUserToMachineUserService
    {
        Task<IEnumerable<SystemUserToMachineUserDTO>> GetMachineUsers(Guid query);
        Task<SystemUserToMachineUserDTO> Create(SystemUserToMachineUserDTO query);
        Task<SystemUserToMachineUserDTO> Delete(Guid query);
    }
}
using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Interfaces
{
    public interface IMachineUserService
    {
        Task<MachineUserDTO> Get(Guid query);
        Task<IEnumerable<MachineUserDTO>> GetMachineAllMachineUsers(Guid query);
        Task<MachineUserDTO> Create(MachineUserDTO query);
        Task<MachineUserDTO> Update(MachineUserDTO query);
        Task<MachineUserDTO> Delete(Guid query);
    }
}
using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Interfaces
{
    public interface IMachineService
    {
        Task<MachineDTO> Get(Guid query);
        Task<MachineDTO> Create(MachineDTO query);
        Task<MachineDTO> Update(MachineDTO query);
        Task<MachineDTO> Delete(Guid query);
    }
}
using DataManagerAPI.Core.Entities;

namespace DataManagerAPI.Core.Interfaces
{
    public interface IMachineUserRepository
    {
        Task<MachineUser> GetAsync(Guid guid);
        Task<MachineUser> AddAsync(MachineUser machineUser);
        Task<MachineUser> UpdateAsync(MachineUser machineUser);
        Task<MachineUser> DeleteAsync(Guid guid);
        Task<IEnumerable<MachineUser>> GetMachineUsersOfMachineAsync(Guid machineGuid);
    }
}

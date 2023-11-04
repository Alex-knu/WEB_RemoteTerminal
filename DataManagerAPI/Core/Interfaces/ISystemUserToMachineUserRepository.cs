using DataManagerAPI.Core.Entities;
using System;

namespace DataManagerAPI.Core.Interfaces
{
    public interface ISystemUserToMachineUserRepository
    {
        Task<IEnumerable<SystemUserToMachineUser>> GetSystemUserMachineUsersAsync(Guid systemUserGuid);
        Task<SystemUserToMachineUser> AddMachineUserForSystemUserAsync(SystemUserToMachineUser systemUserToMachineUser);
        Task<SystemUserToMachineUser> DeleteMachineUserForSystemUserAsync(SystemUserToMachineUser systemUserToMachineUser);
    }
}

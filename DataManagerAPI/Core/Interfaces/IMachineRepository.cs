using DataManagerAPI.Core.Entities;
using System;

namespace DataManagerAPI.Core.Interfaces
{
    public interface IMachineRepository
    {
        Task<Machine> GetAsync(Guid guid);
        Task<Machine> AddAsync(Machine machine);
        Task<Machine> UpdateAsync(Machine machine);
        Task<Machine> DeleteAsync(Guid guid);
    }
}

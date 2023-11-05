using DataManagerAPI.Core.Entities;

namespace DataManagerAPI.Core.Interfaces
{
    public interface ICommandHistoryRepository
    {
        Task<CommandHistory> GetAsync(Guid guid);
        Task<CommandHistory> AddAsync(CommandHistory commandHistory);
        Task<CommandHistory> UpdateAsync(CommandHistory commandHistory);
        Task<CommandHistory> DeleteAsync(Guid guid);
        Task<IEnumerable<CommandHistory>> GetCommandHistoryOfMachineUserAsync(Guid machineUserGuid);
    }
}

using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Interfaces
{
    public interface ICommandHistoryService
    {
        Task<IEnumerable<CommandHistoryDTO>> GetMachineUserCommands(Guid query);
        Task<CommandHistoryDTO> Create(CommandHistoryDTO query);
    }
}
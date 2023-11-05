using GatewayAPI.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Core.Interfaces
{
    public interface ICommandHistoryService
    {
        Task<IEnumerable<CommandHistoryDTO>> GetMachineUserCommands(Guid query);
        Task<CommandHistoryDTO> Create(CommandHistoryDTO query);
    }
}
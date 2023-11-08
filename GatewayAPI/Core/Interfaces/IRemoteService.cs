using GatewayAPI.Core.Models.DTO;

namespace GatewayAPI.Core.Interfaces
{
    public interface IRemoteService
    {
        Task<RunCommandResponceDTO> RunCommandAsync(RunCommandDTO query);
    }
}
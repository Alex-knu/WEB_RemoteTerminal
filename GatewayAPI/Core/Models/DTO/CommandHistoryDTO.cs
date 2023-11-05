namespace GatewayAPI.Core.Models.DTO
{
    public class CommandHistoryDTO
    {
        public Guid Id { get; set; }
        public MachineUserDTO? MachineUser { get; set; }
        public Guid MachineUserId { get; set; }
        public string Command { get; set; }
        public DateTime Time { get; set; }
    }
}
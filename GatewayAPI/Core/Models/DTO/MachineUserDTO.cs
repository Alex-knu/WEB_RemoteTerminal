namespace GatewayAPI.Core.Models.DTO
{
    public class MachineUserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public MachineDTO? Machine { get; set; }
        public Guid MachineId { get; set; }
    }
}
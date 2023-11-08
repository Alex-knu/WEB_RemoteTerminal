namespace GatewayAPI.Core.Models.DTO
{
    public class SystemUserToMachineUserDTO
    {
        public Guid? Id { get; set; }
        public Guid? SystemUserId { get; set; }
        public MachineUserDTO? MachineUser { get; set; }
        public Guid? MachineUserId { get; set; }

    }
}

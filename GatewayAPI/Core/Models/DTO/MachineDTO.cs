namespace GatewayAPI.Core.Models.DTO
{
    public class MachineDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
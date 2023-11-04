namespace GatewayAPI.Core.Models.DTO
{
    public class RunCommandDTO
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool? UseSSHKey { get; set; }
        public string? RootPassword { get; set; }
        public string Command { get; set; }
    }
}
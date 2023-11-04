namespace GatewayAPI.Extentions.Models
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
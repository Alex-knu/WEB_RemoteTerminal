namespace GatewayAPI.Extentions.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }
}
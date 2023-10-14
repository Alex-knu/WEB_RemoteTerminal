using IdentityApi.Core.DTO;
using IdentityApi.Core.Models;

namespace IdentityApi.Core.Interfaces.Services
{
    public interface IAuthenticateSevice
    {
        Task<string> Register(RegisterModel model);
        Task RegisterAdmin(RegisterModel model);
        Task<TokenInfoDTO> Login(LoginModel model);
    }
}
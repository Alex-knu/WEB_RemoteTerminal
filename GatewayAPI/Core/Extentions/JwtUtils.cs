using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GatewayAPI.Extentions.Models;
using GatewayAPI.Extentions.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace GatewayAPI.Extentions.Extentions
{
    public static class JwtUtils
    {
        public static string SecretKey { get; set; }

        public static UserInfo GetUserInfo(HttpContext httpContext)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenHeader = httpContext.Request.Headers["Authorization"];

            if (!tokenHeader.ToString().StartsWith("Bearer "))
            {
                throw new BadRequestException("Invalid token");
            }

            var token = tokenHeader.ToString().Substring(7);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
                
                return new UserInfo
                {
                    UserId = new Guid(claimsPrincipal.FindFirst("UserIdentifier")?.Value),
                    UserName = claimsPrincipal.FindFirst("UserName")?.Value,
                    Roles = claimsPrincipal.FindAll("Roles").Select(c => c.Value).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Invalid token");
            }
        }
    }
}
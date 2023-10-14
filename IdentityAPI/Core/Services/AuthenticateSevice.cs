using IdentityApi.Core.DTO;
using IdentityApi.Core.Interfaces.Services;
using IdentityApi.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityApi.Core.Services
{
    public class AuthenticateSevice : IAuthenticateSevice
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateSevice(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<TokenInfoDTO> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                throw new Exception("Wrong user credentials!");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                 {
                     new Claim("UserIdentifier", user.Id),
                     new Claim("UserName", user.UserName),
                     new Claim("Jti", Guid.NewGuid().ToString()),
                 };

            if (userRoles.Any())
            {
                authClaims.Add(new Claim("Roles", JsonSerializer.Serialize(userRoles.ToList()), JsonClaimValueTypes.JsonArray));
            }

            var token = GetToken(authClaims);

            return new TokenInfoDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public async Task<string> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                throw new Exception("User already exists!");
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User creation failed! Please check user details and try again.");
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRolesAsync(user, new List<string> { UserRoles.User });
            }

            return (await _userManager.FindByNameAsync(model.Username)).Id;
        }

        public async Task RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                throw new Exception("User already exists!");
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User creation failed! Please check user details and try again.");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRolesAsync(user, new List<string> { UserRoles.Admin, UserRoles.User });
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
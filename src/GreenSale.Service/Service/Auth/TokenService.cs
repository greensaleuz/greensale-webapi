using GreenSale.DataAccess.ViewModels.UserRoles;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenSale.Service.Service.Auth;

public class TokenService : ITokenService
{
    private IConfigurationSection _config;
    public TokenService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }

    public string GenerateToken(UserRoleViewModel user)
    {
        var identityClaims = new Claim[]
        {
            new Claim ("Id", user.Id.ToString()),
            new Claim ("FirstName", user.FirstName),
            new Claim ("LastName", user.LastName),
            new Claim ("PhoneNumber", user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.RoleName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("23f926fb-dcd2-49f4-8fe2-992aac18f08f"!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        int expiresHours = 24!;

        var token = new JwtSecurityToken(
            issuer: "http://GreenSale.uz",
            audience: "GreenSale",
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GreenSale.WebApi.Configurations;

public static class JwtConfiguration
{
    public static void ConfigureJwtAuth(this WebApplicationBuilder bulder)
    {
        var config = bulder.Configuration.GetSection("Jwt");

        bulder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = "http://GreenSale.uz",
                ValidateAudience = true,
                ValidAudience = "GreenSale",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("23f926fb-dcd2-49f4-8fe2-992aac18f08f"!))
            };
        });
    }
}

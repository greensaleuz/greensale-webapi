using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.Repositories.Users;

namespace GreenSale.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

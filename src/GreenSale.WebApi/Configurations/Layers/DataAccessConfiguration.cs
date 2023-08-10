using GreenSale.DataAccess.Interfaces.Categories;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.Repositories.Categories;
using GreenSale.DataAccess.Repositories.Users;

namespace GreenSale.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            //-> DI containers, IoC containers
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

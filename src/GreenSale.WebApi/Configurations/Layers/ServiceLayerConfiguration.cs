using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Categories;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Notifications;
using GreenSale.Service.Service.Auth;

namespace GreenSale.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            //-> DI containers, IoC containers
            builder.Services.AddScoped<IAuthServices, AuthServise>();
        }
    }
}

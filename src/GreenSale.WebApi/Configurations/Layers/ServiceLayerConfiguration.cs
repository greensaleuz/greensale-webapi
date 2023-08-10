using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Notifications;
using GreenSale.Service.Service.Auth;
using GreenSale.Service.Service.Categories;
using GreenSale.Service.Service.Common;
using GreenSale.Service.Service.Notifications;

namespace GreenSale.WebApi.Configurations.Layers;

public static class ServiceLayerConfiguration
{
    public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthServices, AuthServise>();
        builder.Services.AddScoped<ISmsSender, SmsSender>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IPaginator, Pagination>();
    }
}

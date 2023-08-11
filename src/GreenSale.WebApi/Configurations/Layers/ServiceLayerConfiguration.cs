using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Interfaces.Categories;
using GreenSale.Service.Interfaces.Common;
using GreenSale.Service.Interfaces.Notifications;
using GreenSale.Service.Interfaces.Roles;
using GreenSale.Service.Interfaces.Storages;
using GreenSale.Service.Service.Auth;
using GreenSale.Service.Service.Categories;
using GreenSale.Service.Service.Common;
using GreenSale.Service.Service.Notifications;
using GreenSale.Service.Service.Roles;
using GreenSale.Service.Service.Storages;
using System.Data.SqlTypes;

namespace GreenSale.WebApi.Configurations.Layers;

public static class ServiceLayerConfiguration
{
    public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISmsSender, SmsSender>();
        builder.Services.AddScoped<IAuthServices, AuthServise>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IPaginator, Pagination>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IStoragesService, StorageService>();
        builder.Services.AddScoped<IFileService, FileService>();
    }
}

using GreenSale.DataAccess.Interfaces.BuyerPosts;
using GreenSale.DataAccess.Interfaces.Categories;
using GreenSale.DataAccess.Interfaces.Roles;
using GreenSale.DataAccess.Interfaces.SellerPosts;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.Repositories.BuyerPosts;
using GreenSale.DataAccess.Repositories.Categories;
using GreenSale.DataAccess.Repositories.Roles;
using GreenSale.DataAccess.Repositories.SellerPosts;
using GreenSale.DataAccess.Repositories.Storages;
using GreenSale.DataAccess.Repositories.Users;

namespace GreenSale.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepositories>();
            builder.Services.AddScoped<IUserRoles, UserRoleRepository>();
            builder.Services.AddScoped<ISellerPostsRepository, SellerPostRepository>();
            builder.Services.AddScoped<ISellerPostImageRepository, SellerPostImageRepository>();
            builder.Services.AddScoped<ISellerPostsRepository, SellerPostRepository>();
            builder.Services.AddScoped<IStorageRepository, StorageRepository>();
            builder.Services.AddScoped<IBuyerPostRepository, BuyerPostsRepository>();
            builder.Services.AddScoped<IBuyerPostImageRepository,BuyerPostImageRepository>();
        }
    }
}

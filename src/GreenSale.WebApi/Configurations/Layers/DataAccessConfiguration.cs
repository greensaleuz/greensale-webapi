﻿using GreenSale.DataAccess.Interfaces.Categories;
using GreenSale.DataAccess.Interfaces.Roles;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.Repositories.Categories;
using GreenSale.DataAccess.Repositories.Roles;
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
            builder.Services.AddScoped<IStorageRepository, StorageRepository>();
        }
    }
}

using GreenSale.DataAccess.Interfaces.Users;
using GreenSale.DataAccess.Repositories.Users;
using GreenSale.Service.Interfaces.Auth;
using GreenSale.Service.Service.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


//-->dataacces

builder.Services.AddScoped<IUserRepository, UserRepository>();

//-> service
builder.Services.AddScoped<IAuthServices, AuthServise>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

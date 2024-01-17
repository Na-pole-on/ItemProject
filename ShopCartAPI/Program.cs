using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopCartAPI.Data;
using ShopCartAPI.Repositories;
using ShopCartAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaulConnection");

builder.Services.AddDbContext<CartDbContext>(options
    => options.UseSqlServer(connString));

/*
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7267/";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
 */

builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();

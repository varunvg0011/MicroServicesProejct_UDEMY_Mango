
using Mango.Services.AuthAPI.data;
using Mango.Services.AuthAPI.IServices;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnString"));
    //we have to tell this application that hey, we are using EF core with identity in this project
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();

//AddEntityFrameworkStores<ApplicationDBContext>(); acets as a bridge btw EFcore and .net identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//we are adding this in this project because we are using AuthAPI for authentication and authorizaion
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();


void ApplyMigration()
{
    //this will get all the servuces
    using (var scope = app.Services.CreateScope())
    {
        //now from this service we only want the services related to AppDbContext
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        //check if pending migrations
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
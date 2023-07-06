using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//configuring our ef core data class. Here option is saying that we are using SQL server for our DB
//we can hard code the conn string inside UseSqlServer("") but its not a good idea so we define in
//appsettings.json. And then access tat conn string in ""
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnString"));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();


//we are adding this method here to introduce automatic updation of data in case we have some pending
//migrations to be updated in the database
void ApplyMigration()
{
    //this will get all the servuces
    using (var scope = app.Services.CreateScope())
    {
        //now from this service we only want the services related to AppDbContext
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        //check if pending migrations
        if(_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
using Mango.WebApp.Models.Utility;
using Mango.WebApp.Service;
using Mango.WebApp.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//Registering our IHttpClientFactory
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
//then we need to configure our coupon Service using AddHttpClient that we added above
builder.Services.AddHttpClient<ICouponService, CouponService>();



//here we are populating our CouponAPIBase with CouponURL. This will retrieve the value and assign it
// to CouponAPIBase at the time of when services are being configured
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPIBase"];


builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICouponService, CouponService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

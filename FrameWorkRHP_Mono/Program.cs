using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Models;
using FrameWorkRHP_Mono.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

/*********************XXXXX*************************/
/*********************End XXXXX*************************/

/*********************Add Custom Additional*************************/
builder.Services.AddDependencyInjectionServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));  // auto mapper 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // agar timestamp postgre bisa di input sesuai format 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
/*********************End Custom Additional*************************/


/*********************Original .Net*************************/
// Add services to the container.
builder.Services.AddRazorPages(); 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
/*********************End Original .Net*************************/


/*********************Kebutuhan Session*************************/
builder.Services.AddDistributedMemoryCache(); // digunakan untuk sharing session dalam 1 server, kemungkinan bisa untuk sso based on server yang sama, kalau server beda pakai redis / db
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true; //untuk menjaga dari xss jika di akses hanya dari HTTP, kalau udah https comment aja
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.LoginPath = new PathString("/Login");
            options.AccessDeniedPath = new PathString("/denied");
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
        });

/*********************End Kebutuhan Session*************************/


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>(); 

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



// === CUSTOM ADDITIONAL ===  

// === END CUSTOM ADDITIONAL

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); 
app.UseSession();
app.UseAuthorization();

 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

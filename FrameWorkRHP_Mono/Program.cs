using FrameWorkRHP_Mono.Models;
using FrameWorkRHP_Mono.Services; 

var builder = WebApplication.CreateBuilder(args);
 
/*Add Custom Additional*/
builder.Services.AddDependencyInjectionServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));  // auto mapper 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // agar timestamp postgre bisa di input sesuai format

/*End Custom Additional*/

// Add services to the container.
builder.Services.AddRazorPages(); 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();



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

app.UseAuthorization();
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

using DinkToPdf;
using DinkToPdf.Contracts;
using DropZone_BackPanel.Contracts;
using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.Repository;
using DropZone_BackPanel.Services.Dapper;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using DropZone_BackPanel.ERPServices.PersonData;
using DropSpace.Context;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Mvc.Razor;
using DropZone_BackPanel.Helpers.LinkEncrypt;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using DropZone_BackPanel.ERPServices.MasterData;
using DropZone_BackPanel.Services.MasterData.Interfaces;
using DropZone_BackPanel.ERPServices.ReportData.Interface;
using DropZone_BackPanel.ERPServices.ReportData;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //options.JsonSerializerOptions.MaxDepth = 10;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
}).AddRazorRuntimeCompilation();
#endregion

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<DropSpaceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity for user and role management
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DropSpaceDbContext>()
    .AddDefaultTokenProviders();

// Configure Kestrel options for large file uploads
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2147483648; // Set to 2 GB (in bytes)
});

#region Auth Related Settings
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 20;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
// Configure application cookie for login redirection
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Auth/Account/Login"; // Redirects unauthenticated users to the login page
    options.AccessDeniedPath = "/Auth/AccessDenied"; // Redirect for unauthorized access
    options.SlidingExpiration = true;
});
#endregion

// Add HttpClient and WebHostEnvironment
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);
builder.Services.AddDistributedMemoryCache();

#region App metrics
builder.Services.Configure<KestrelServerOptions>(opt => { opt.AllowSynchronousIO = true; });
//builder.Services.AddMetrics();
#endregion
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


// Register application-specific services
builder.Services.AddScoped<IDapper, DropZone_BackPanel.Services.Dapper.Dapper>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ISettingsService, SettingsService>();
builder.Services.AddScoped<IUserInfoes, UserInfoes>();
builder.Services.AddScoped<IPersonData, PersonData>();
builder.Services.AddScoped<IMasterData, MasterDataService>();

// Register PDF Converter
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


#region Areas Config
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});
#endregion

#region Configure Application

var app = builder.Build();

#region For Seed Value
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        var context = services.GetRequiredService<DropSpaceDbContext>();
        await SeedData.SeedAsync(userManager, roleManager, context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during seeding: {ex.Message}");
    }
}
#endregion

// Middleware pipeline configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEncryptDecryptQueryStringsMiddleware();
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion

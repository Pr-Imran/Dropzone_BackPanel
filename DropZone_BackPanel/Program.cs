using DinkToPdf;
using DinkToPdf.Contracts;
using DropZone_BackPanel.Context;
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
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using DropZone_BackPanel.ERPServices.MasterData;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

// Configure the database context
builder.Services.AddDbContext<DropZoneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity for user and role management
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<DropZoneDbContext>()
    .AddDefaultTokenProviders();

// Add HTTP Context Accessor
builder.Services.AddHttpContextAccessor();

// Add MVC with Razor Runtime Compilation and Newtonsoft JSON for serialization
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
    });
builder.Services.AddRazorPages();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("_myAllowSpecificOrigins",
//     builder => builder
//     .AllowAnyOrigin()
//     .AllowAnyMethod()
//     .AllowAnyHeader()
//        );
//});
// Configure application cookie for login redirection
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Auth/Account/Login"; // Redirects unauthenticated users to the login page
    options.AccessDeniedPath = "/Auth/AccessDenied"; // Redirect for unauthorized access
    options.SlidingExpiration = true;
});

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Register application-specific services
builder.Services.AddScoped<IDapper, DropZone_BackPanel.Services.Dapper.Dapper>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserInfoes, UserInfoes>();
builder.Services.AddScoped<IPersonData, PersonData>();
builder.Services.AddScoped<IMasterData, MasterDataService>();

// Register PDF Converter
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

#endregion

#region Configure Application

var app = builder.Build();

// Seed database (uncomment if required during initialization)
//var userManager = app.Services.GetRequiredService<UserManager<ApplicationUser>>();
//var roleManager = app.Services.GetRequiredService<RoleManager<ApplicationRole>>();
//SeedData.Seed(userManager, roleManager);

// Middleware pipeline configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCors("_myAllowSpecificOrigins");
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization(); 

// Configure routing for areas and default controller
app.MapRazorPages();
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion

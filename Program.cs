using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TMDT.Data;
using Microsoft.AspNetCore.Identity;
using TMDT.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using TMDT.Services;
using Microsoft.AspNetCore.Authentication.Facebook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("vi-VN")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddRazorPages();

builder.Services.AddDbContext<TMDTDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TMDTDbContext") ?? throw new InvalidOperationException("Connection string 'TMDTDbContext' not found.")));

builder.Services.AddDbContext<LOGINDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LOGINDbContext") ?? throw new InvalidOperationException("Connection string 'LOGINDbContext' not found.")));

builder.Services.AddIdentity<LOGINUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LOGINDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddRoles<IdentityRole>();

// Add authentication services
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
        options.AccessDeniedPath = "/AccessDeniedPathInfo";
    })
    .AddGoogle(gopts =>
    {
        gopts.ClientId = "362498821723-ljktm2gcqhj1ml2g9vop18irk20g34ib.apps.googleusercontent.com";
        gopts.ClientSecret = "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";
    });

// Register email sender
builder.Services.AddSingleton<IEmailSender, EmailSender>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();

app.UseAuthentication(); // This needs to be called before UseAuthorization
app.UseAuthorization();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "admin_area",
    areaName: "Identity",
    pattern: "Identity/{controller=Home}/{action=Index}/{id?}");

app.Run();

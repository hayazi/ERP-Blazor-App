using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using ERPBlazorApp.Data;
using MudBlazor;
using MudBlazor.Services;
using ERPBlazorApp.HumanResource.Data;
using ERPBlazorApp.AAA.Data;
using ERPBlazorApp.AAA.Services;
using ERPBlazorApp.AAA.Auth;
using ERPBlazorApp.Accounting.Data;
using ERPBlazorApp.Accounting.Services;
using ERPBlazorApp.CRM.Data;
using ERPBlazorApp.CRM.Services;
using ERPBlazorApp.Sales.Data;
using ERPBlazorApp.Sales.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddDbContext<HumanResourceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPBlazorDb")));
builder.Services.AddDbContext<ERPBlazorApp.Inventory.Data.InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPBlazorDb")));
builder.Services.AddDbContext<AAADbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPBlazorDb")));
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPBlazorDb")));
builder.Services.AddDbContext<CRMDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPBlazorDb")));
builder.Services.AddDbContext<SaleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPBlazorDb")));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "ERPBlazorApp:";
});
builder.Services.AddScoped<ERPBlazorApp.HumanResource.Services.EmployeeService>();
builder.Services.AddScoped<ERPBlazorApp.HumanResource.Services.DepartmentService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.ProductService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.CategoryService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.InboundService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.OutboundService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.KardexService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.SupplierService>();
builder.Services.AddScoped<ERPBlazorApp.Inventory.Services.CustomerService>();
builder.Services.AddScoped<ERPBlazorApp.AAA.Services.UserService>();
builder.Services.AddScoped<ERPBlazorApp.AAA.Services.RoleService>();
builder.Services.AddScoped<ERPBlazorApp.AAA.Services.PermissionService>();
builder.Services.AddScoped<ERPBlazorApp.AAA.Services.AccountService>();
builder.Services.AddScoped<ERPBlazorApp.AAA.Auth.AuthService>();
builder.Services.AddScoped<ERPBlazorApp.Accounting.Services.ChartOfAccountService>();
builder.Services.AddScoped<ERPBlazorApp.Accounting.Services.JournalEntryService>();
builder.Services.AddScoped<ERPBlazorApp.Accounting.Services.FiscalYearService>();
builder.Services.AddScoped<ERPBlazorApp.Accounting.Services.BudgetService>();
builder.Services.AddScoped<ERPBlazorApp.Accounting.Services.TrialBalanceService>();
builder.Services.AddScoped<ERPBlazorApp.CRM.Services.LeadService>();
builder.Services.AddScoped<ERPBlazorApp.CRM.Services.OpportunityService>();
builder.Services.AddScoped<ERPBlazorApp.CRM.Services.ActivityService>();
builder.Services.AddScoped<ERPBlazorApp.CRM.Services.CampaignService>();
builder.Services.AddScoped<ERPBlazorApp.Sales.Services.SaleService>();
builder.Services.AddScoped<ERPBlazorApp.Sales.Services.SaleItemService>();
builder.Services.AddScoped<ERPBlazorApp.Sales.Services.PaymentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await AAADataSeeder.SeedAsync(scope.ServiceProvider);
    await AccountingDataSeeder.SeedAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en"),
    SupportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("fa") },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("fa") },
    RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new ERPBlazorApp.QueryStringCultureProvider()
    }
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

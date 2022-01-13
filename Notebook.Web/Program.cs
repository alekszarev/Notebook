using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Data.Models;
using Notebook.Data.Seeding;
using Notebook.Service;
using Notebook.Service.Contracts;
using Notebook.Service.Email;
using Notebook.Service.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serviceCollection = builder.Services.AddDbContext<NotebookContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(IdentityOptionsProvider.GetIdentityOptions)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<NotebookContext>();
builder.Services.AddSingleton<IEmailConfiguration>(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.Configure<CookiePolicyOptions>(
   options =>
   {
       options.CheckConsentNeeded = context => true;
       options.MinimumSameSitePolicy = SameSiteMode.None;
   });

builder.Services.AddControllersWithViews(
   options =>
   {
       options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
   });
builder.Services.AddRazorPages();

var app = builder.Build();
    using (var serviceScope = app.Services.CreateScope())
    {
         var dbContext = serviceScope.ServiceProvider.GetRequiredService<NotebookContext>();
         dbContext.Database.Migrate();
         if (!dbContext.Roles.Any(x => x.Name == "Owner"))
         {
             new NotebookContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
         }
    }

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

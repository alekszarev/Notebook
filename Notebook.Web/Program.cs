using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notebook.Data;
using Notebook.Data.Models;
using Notebook.Data.Seeding;
using Notebook.Service.Contracts;
using Notebook.Service.Email;
using Notebook.Service.Services;
using Notebook.Service.Services.Calendar;
using Notebook.Web.Models.ModelMapper;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

//TODO: MAKE CLASS FOR DARK LIGHT MODE!



// Add services to the container.
builder.Logging.ClearProviders();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NotebookContext>(options =>
    options.UseSqlServer(connectionString));var serviceCollection = builder.Services.AddDbContext<IDatabase, NotebookContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers()
      .AddNewtonsoftJson(
          options =>
          {
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          });

builder.Services.AddDefaultIdentity<User>(IdentityOptionsProvider.GetIdentityOptions)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<NotebookContext>();
builder.Services.AddSingleton<IEmailConfiguration>(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddScoped<ModelMapper>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<ICalendarOldService, CalendarOldService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDayService, DayService>();
builder.Services.AddScoped<IHourService, HourService>();
builder.Services.AddScoped<IMonthService, MonthService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IProcedureService, ProcedureService>();
builder.Services.AddScoped<IYearService, YearService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserBufferService, UserBufferService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IConversationService, ConversationService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";
    options.SlidingExpiration = true;
});

builder.Services.Configure<CookiePolicyOptions>(
   options =>
   {
       options.CheckConsentNeeded = context => true;
       options.MinimumSameSitePolicy = SameSiteMode.None;

   });

builder.Services.AddControllersWithViews(
   options =>
   {
       //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
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
    app.UseSwagger();
    app.UseSwaggerUI();
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

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notebook.Data;
using Notebook.Service.Contracts;
using Notebook.Service.Services;
using Notebook.Service.Services.Calendar;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IDatabase, NotebookContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers()
      .AddNewtonsoftJson(
          options =>
          {
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          });

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

app.Run();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

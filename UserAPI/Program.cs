using Common.Domain;
using Common.Repository;
using Serilog;
using Serilog.Events;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using UserServices;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("Logs/Information-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
    .WriteTo.File("Logs/Log-error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUserServices();
builder.Services.AddFluentValidationAutoValidation();

builder.Host.UseSerilog();
WebApplication app = null;

try
{
    app = builder.Build();
}
catch (Exception ex)
{
    Log.Error(ex.Message);
}

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

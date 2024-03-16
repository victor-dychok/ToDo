using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ToDo;
using ToDoBL;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Common.Api;
using Serilog;
using Common.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

try
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddToDoServices();

    builder.Services.AddFluentValidationAutoValidation();

    builder.Services.AddTodoDatabase(builder.Configuration);

    var app = builder.Build();

    app.UseExeptionHendler();

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
}
catch(Exception ex)
{
    Log.Error(ex, "Run error");
    throw;
}
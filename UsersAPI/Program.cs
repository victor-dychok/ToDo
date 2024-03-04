using Common.Domain;
using Common.Repository;
using UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IRepository<User>, BaseRepository<User>>();
builder.Services.AddTransient<IUserService, UserService>();
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

app.MapGet("/users", (IUserService users) =>
{
    return users.GetList(0, null, 10);

}).WithDisplayName("Users");

app.Run();

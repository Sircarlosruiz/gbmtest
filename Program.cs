using gbmtest;
using gbmtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ProyectContext>(options => options.UseInMemoryDatabase("ProyectDB"));
builder.Services.AddDbContext<ProyectContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddControllers();

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", ([FromServices] ProyectContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos creada correctamente" + dbContext.Database.IsInMemory());
});

app.UseRouting();

app.MapControllers();

app.Run();

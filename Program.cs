using gbmtest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ProyectContext>(options => options.UseInMemoryDatabase("ProyectDB"));
builder.Services.AddDbContext<ProyectContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// app.MapGet("/dbconexion", ([FromServices] ProyectContext dbContext) =>
// {
//     dbContext.Database.EnsureCreated();
//     return Results.Ok("Base de datos creada correctamente" + dbContext.Database.IsInMemory());
// });

//Clientes
//Get
app.MapGet("/api/clientes", async ([FromServices] ProyectContext dbContext) =>
{
    var clientes = await dbContext.Clientes.ToListAsync();
    return Results.Ok(clientes);
});

//Factura
//Get
app.MapGet("/api/facturas", async ([FromServices] ProyectContext dbContext) =>
{
    var facturas = await dbContext.Facturas.ToListAsync();
    return Results.Ok(facturas);
});

//Productos
//Get
app.MapGet("/api/productos", async ([FromServices] ProyectContext dbContext) =>
{
    var productos = await dbContext.Productos.ToListAsync();
    return Results.Ok(productos);
});

//Tasa de Cambio
//Get
app.MapGet("/api/tasacambio", async ([FromServices] ProyectContext dbContext) =>
{
    var tasaCambio = await dbContext.TasasDeCambio.ToListAsync();
    return Results.Ok(tasaCambio);
});

//Detalle Factura
//Get
app.MapGet("/api/detallefactura", async ([FromServices] ProyectContext dbContext) =>
{
    var detalleFacturas = await dbContext.DetallesFactura.ToListAsync();
    return Results.Ok(detalleFacturas);
});


app.Run();

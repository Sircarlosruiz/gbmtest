using gbmtest;
using gbmtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ProyectContext>(options => options.UseInMemoryDatabase("ProyectDB"));
builder.Services.AddDbContext<ProyectContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", ([FromServices] ProyectContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos creada correctamente" + dbContext.Database.IsInMemory());
});

//Clientes
//Get
app.MapGet("/api/clientes", async ([FromServices] ProyectContext dbContext) =>
{
    var clientes = await dbContext.Clientes.Include(cliente => cliente.Facturas).ToListAsync();
    return Results.Ok(clientes);
});

//Post
app.MapPost("/api/clientes", async ([FromServices] ProyectContext dbContext, [FromBody] Cliente cliente) =>
{
    cliente.Id = Guid.NewGuid();
    await dbContext.Clientes.AddAsync(cliente);
    await dbContext.SaveChangesAsync();
    return Results.Ok(cliente);
});

//Factura
//Get
app.MapGet("/api/facturas", async ([FromServices] ProyectContext dbContext) =>
{
    var facturas = await dbContext.Facturas.Include(factura => factura.DetallesFactura).ToListAsync();
    return Results.Ok(facturas);
});

//Post
app.MapPost("/api/facturas", async ([FromServices] ProyectContext dbContext, [FromBody] Factura factura) =>
{
    factura.Id = Guid.NewGuid();
    await dbContext.Facturas.AddAsync(factura);
    await dbContext.SaveChangesAsync();
    return Results.Ok(factura);
});

//Productos
//Get
app.MapGet("/api/productos", async ([FromServices] ProyectContext dbContext) =>
{
    var productos = await dbContext.Productos.Include(producto => producto.DetallesFacturas).ToListAsync();
    return Results.Ok(productos);
});

//Post
app.MapPost("/api/productos", async ([FromServices] ProyectContext dbContext, [FromBody] Producto producto) =>
{
    producto.Id = Guid.NewGuid();
    await dbContext.Productos.AddAsync(producto);
    await dbContext.SaveChangesAsync();
    return Results.Ok(producto);
});

//Tasa de Cambio
//Get
app.MapGet("/api/tasacambio", async ([FromServices] ProyectContext dbContext) =>
{
    var tasaCambio = await dbContext.TasasDeCambio.ToListAsync();
    return Results.Ok(tasaCambio);
});

//Post
app.MapPost("/api/tasacambio", async ([FromServices] ProyectContext dbContext, [FromBody] TasaDeCambio tasaCambio) =>
{
    tasaCambio.Id = Guid.NewGuid();
    await dbContext.TasasDeCambio.AddAsync(tasaCambio);
    await dbContext.SaveChangesAsync();
    return Results.Ok(tasaCambio);
});

//Detalle Factura
//Get
app.MapGet("/api/detallefactura", async ([FromServices] ProyectContext dbContext) =>
{
    var detalleFacturas = await dbContext.DetallesFactura
        .Include(detalle => detalle.Factura)
        .Include(detalle => detalle.Producto)
        .ToListAsync();
    return Results.Ok(detalleFacturas);
});

//Post
app.MapPost("/api/detallefactura", async ([FromServices] ProyectContext dbContext, [FromBody] DetalleFactura detallesFacturas) =>
{
    detallesFacturas.Id = Guid.NewGuid();
    await dbContext.DetallesFactura.AddAsync(detallesFacturas);
    await dbContext.SaveChangesAsync();
    return Results.Ok(detallesFacturas);
});


app.Run();

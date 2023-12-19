using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gbmtest.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "Codigo", "Direccion", "Nombre" },
                values: new object[,]
                {
                    { new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20ca"), "C1", "Managua", "Cliente 1" },
                    { new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"), "B1", "Granaga", "Cliente 2" }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "Id", "Descripcion", "PrecioCordobas", "PrecioDolares", "SKU" },
                values: new object[,]
                {
                    { new Guid("dc131c19-84af-4627-902a-08e3d5c98791"), "Producto 1", 100m, 10m, "P1" },
                    { new Guid("dc131c19-84af-4627-902a-08e3d5c98792"), "Producto 2", 200m, 20m, "P2" }
                });

            migrationBuilder.InsertData(
                table: "TasaDeCambio",
                columns: new[] { "Id", "Fecha", "Valor" },
                values: new object[] { new Guid("f789837c-ddd3-4451-be64-57996acf89ce"), new DateTime(2023, 12, 19, 3, 59, 27, 864, DateTimeKind.Local).AddTicks(7158), 36m });

            migrationBuilder.InsertData(
                table: "Factura",
                columns: new[] { "Id", "ClienteId", "Fecha" },
                values: new object[,]
                {
                    { new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"), new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"), new DateTime(2023, 12, 19, 3, 59, 27, 864, DateTimeKind.Local).AddTicks(7144) },
                    { new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"), new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20ca"), new DateTime(2023, 12, 19, 3, 59, 27, 864, DateTimeKind.Local).AddTicks(7114) }
                });

            migrationBuilder.InsertData(
                table: "DetalleFactura",
                columns: new[] { "Id", "Cantidad", "FacturaId", "PrecioUnitario", "ProductoId" },
                values: new object[,]
                {
                    { new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cd"), 1, new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"), 100m, new Guid("dc131c19-84af-4627-902a-08e3d5c98791") },
                    { new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20ce"), 2, new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"), 200m, new Guid("dc131c19-84af-4627-902a-08e3d5c98792") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cd"));

            migrationBuilder.DeleteData(
                table: "DetalleFactura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20ce"));

            migrationBuilder.DeleteData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"));

            migrationBuilder.DeleteData(
                table: "TasaDeCambio",
                keyColumn: "Id",
                keyValue: new Guid("f789837c-ddd3-4451-be64-57996acf89ce"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"));

            migrationBuilder.DeleteData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"));

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: new Guid("dc131c19-84af-4627-902a-08e3d5c98791"));

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: new Guid("dc131c19-84af-4627-902a-08e3d5c98792"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20ca"));
        }
    }
}

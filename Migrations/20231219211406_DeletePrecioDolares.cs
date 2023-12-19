using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gbmtest.Migrations
{
    /// <inheritdoc />
    public partial class DeletePrecioDolares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioDolares",
                table: "Producto");

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 15, 14, 6, 296, DateTimeKind.Local).AddTicks(5998));

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 15, 14, 6, 296, DateTimeKind.Local).AddTicks(5968));

            migrationBuilder.UpdateData(
                table: "TasaDeCambio",
                keyColumn: "Id",
                keyValue: new Guid("f789837c-ddd3-4451-be64-57996acf89ce"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 15, 14, 6, 296, DateTimeKind.Local).AddTicks(6056));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioDolares",
                table: "Producto",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 14, 55, 52, 674, DateTimeKind.Local).AddTicks(2252));

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 14, 55, 52, 674, DateTimeKind.Local).AddTicks(2232));

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: new Guid("dc131c19-84af-4627-902a-08e3d5c98791"),
                column: "PrecioDolares",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "Id",
                keyValue: new Guid("dc131c19-84af-4627-902a-08e3d5c98792"),
                column: "PrecioDolares",
                value: 20m);

            migrationBuilder.UpdateData(
                table: "TasaDeCambio",
                keyColumn: "Id",
                keyValue: new Guid("f789837c-ddd3-4451-be64-57996acf89ce"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 14, 55, 52, 674, DateTimeKind.Local).AddTicks(2263));
        }
    }
}

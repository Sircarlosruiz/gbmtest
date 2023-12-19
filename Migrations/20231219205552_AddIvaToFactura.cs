using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gbmtest.Migrations
{
    /// <inheritdoc />
    public partial class AddIvaToFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Iva",
                table: "Factura",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"),
                columns: new[] { "Fecha", "Iva" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 55, 52, 674, DateTimeKind.Local).AddTicks(2252), 0m });

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"),
                columns: new[] { "Fecha", "Iva" },
                values: new object[] { new DateTime(2023, 12, 19, 14, 55, 52, 674, DateTimeKind.Local).AddTicks(2232), 0m });

            migrationBuilder.UpdateData(
                table: "TasaDeCambio",
                keyColumn: "Id",
                keyValue: new Guid("f789837c-ddd3-4451-be64-57996acf89ce"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 14, 55, 52, 674, DateTimeKind.Local).AddTicks(2263));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iva",
                table: "Factura");

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cb"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 3, 59, 27, 864, DateTimeKind.Local).AddTicks(7144));

            migrationBuilder.UpdateData(
                table: "Factura",
                keyColumn: "Id",
                keyValue: new Guid("3d28b6ea-32e8-497b-9f52-07f211ac20cc"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 3, 59, 27, 864, DateTimeKind.Local).AddTicks(7114));

            migrationBuilder.UpdateData(
                table: "TasaDeCambio",
                keyColumn: "Id",
                keyValue: new Guid("f789837c-ddd3-4451-be64-57996acf89ce"),
                column: "Fecha",
                value: new DateTime(2023, 12, 19, 3, 59, 27, 864, DateTimeKind.Local).AddTicks(7158));
        }
    }
}

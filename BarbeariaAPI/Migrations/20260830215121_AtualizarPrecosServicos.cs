using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarbeariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarPrecosServicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 30, 70.00m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Preco",
                value: 70.00m);

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Preco",
                value: 20.00m);

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 20, 50.00m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 50, 120.00m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 80, 160.00m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 20, 35.00m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 20, 35.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 60, 0m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Preco",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Preco",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 30, 0m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 90, 0m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 90, 0m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 15, 0m });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DuracaoMinutos", "Preco" },
                values: new object[] { 15, 0m });
        }
    }
}

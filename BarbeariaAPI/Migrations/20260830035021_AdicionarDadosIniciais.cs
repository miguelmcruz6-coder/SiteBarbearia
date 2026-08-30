using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarbeariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Servicos",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Barbeiros",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Vinicius Silva Lima" },
                    { 2, "Miguel Miyaki da Cruz" }
                });

            migrationBuilder.InsertData(
                table: "Servicos",
                columns: new[] { "Id", "Descricao", "DuracaoMinutos", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, "Corte de cabelo completo", 60, "Cabelo completo", 0m },
                    { 2, "Serviço completo de barba", 30, "Barba completa", 0m },
                    { 3, "Design de sobrancelha", 15, "Sobrancelha", 0m },
                    { 4, "Corte feito com máquina", 30, "Máquina", 0m },
                    { 5, "Corte completo com hidratação", 90, "Cabelo completo + Hidratação", 0m },
                    { 6, "Combo de cabelo, barba e sobrancelha", 90, "Cabelo completo + Barba + Sobrancelha", 0m },
                    { 7, "Depilação nasal com cera", 15, "Depilação a cera do nariz", 0m },
                    { 8, "Depilação da sobrancelha com cera", 15, "Depilação a cera da sobrancelha", 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Barbeiros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Barbeiros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Servicos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}

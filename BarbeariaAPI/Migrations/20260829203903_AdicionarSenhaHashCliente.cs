using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarbeariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarSenhaHashCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SenhaHash",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "SenhaHash",
                table: "Clientes");
        }
    }
}

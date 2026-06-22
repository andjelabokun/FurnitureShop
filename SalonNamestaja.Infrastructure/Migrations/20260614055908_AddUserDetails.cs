using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonNamestaja.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdresaIsporuke",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PIB",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipKupca",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresaIsporuke",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PIB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipKupca",
                table: "AspNetUsers");
        }
    }
}

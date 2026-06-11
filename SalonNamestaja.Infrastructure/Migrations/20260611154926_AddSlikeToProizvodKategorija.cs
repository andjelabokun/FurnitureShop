using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonNamestaja.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSlikeToProizvodKategorija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlikaUrl",
                table: "Proizvodi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SlikaUrl",
                table: "Kategorije",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlikaUrl",
                table: "Proizvodi");

            migrationBuilder.DropColumn(
                name: "SlikaUrl",
                table: "Kategorije");
        }
    }
}

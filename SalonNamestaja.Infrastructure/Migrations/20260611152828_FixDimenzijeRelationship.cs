using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonNamestaja.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDimenzijeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dimenzije_Proizvodi_ProizvodID",
                table: "Dimenzije");

            migrationBuilder.DropIndex(
                name: "IX_Dimenzije_ProizvodID",
                table: "Dimenzije");

            migrationBuilder.DropColumn(
                name: "ProizvodID",
                table: "Dimenzije");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_DimenzijeID",
                table: "Proizvodi",
                column: "DimenzijeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvodi_Dimenzije_DimenzijeID",
                table: "Proizvodi",
                column: "DimenzijeID",
                principalTable: "Dimenzije",
                principalColumn: "DimenzijeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvodi_Dimenzije_DimenzijeID",
                table: "Proizvodi");

            migrationBuilder.DropIndex(
                name: "IX_Proizvodi_DimenzijeID",
                table: "Proizvodi");

            migrationBuilder.AddColumn<int>(
                name: "ProizvodID",
                table: "Dimenzije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dimenzije_ProizvodID",
                table: "Dimenzije",
                column: "ProizvodID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dimenzije_Proizvodi_ProizvodID",
                table: "Dimenzije",
                column: "ProizvodID",
                principalTable: "Proizvodi",
                principalColumn: "ProizvodID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

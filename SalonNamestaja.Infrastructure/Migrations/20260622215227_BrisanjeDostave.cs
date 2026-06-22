using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonNamestaja.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BrisanjeDostave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dostave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dostave",
                columns: table => new
                {
                    DostavaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PorudzbinaID = table.Column<int>(type: "int", nullable: false),
                    CenaDostave = table.Column<double>(type: "float", nullable: false),
                    DatumDostave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostave", x => x.DostavaID);
                    table.ForeignKey(
                        name: "FK_Dostave_Porudzbine_PorudzbinaID",
                        column: x => x.PorudzbinaID,
                        principalTable: "Porudzbine",
                        principalColumn: "PorudzbinaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dostave_PorudzbinaID",
                table: "Dostave",
                column: "PorudzbinaID",
                unique: true);
        }
    }
}

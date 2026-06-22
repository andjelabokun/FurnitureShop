using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonNamestaja.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveKupacProdavacAddApplicationUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_Kupci_KupacID",
                table: "Porudzbine");

            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_Prodavci_ProdavacID",
                table: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Kupci");

            migrationBuilder.DropTable(
                name: "Prodavci");

            migrationBuilder.DropIndex(
                name: "IX_Porudzbine_KupacID",
                table: "Porudzbine");

            migrationBuilder.DropIndex(
                name: "IX_Porudzbine_ProdavacID",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "KupacID",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "ProdavacID",
                table: "Porudzbine");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Porudzbine",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_ApplicationUserId",
                table: "Porudzbine",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_AspNetUsers_ApplicationUserId",
                table: "Porudzbine",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_AspNetUsers_ApplicationUserId",
                table: "Porudzbine");

            migrationBuilder.DropIndex(
                name: "IX_Porudzbine_ApplicationUserId",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Porudzbine");

            migrationBuilder.AddColumn<int>(
                name: "KupacID",
                table: "Porudzbine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdavacID",
                table: "Porudzbine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kupci",
                columns: table => new
                {
                    KupacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIB = table.Column<int>(type: "int", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipKupca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupci", x => x.KupacID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavci",
                columns: table => new
                {
                    ProdavacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavci", x => x.ProdavacID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_KupacID",
                table: "Porudzbine",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_ProdavacID",
                table: "Porudzbine",
                column: "ProdavacID");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_Kupci_KupacID",
                table: "Porudzbine",
                column: "KupacID",
                principalTable: "Kupci",
                principalColumn: "KupacID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_Prodavci_ProdavacID",
                table: "Porudzbine",
                column: "ProdavacID",
                principalTable: "Prodavci",
                principalColumn: "ProdavacID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

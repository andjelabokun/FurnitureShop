using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonNamestajaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boje",
                columns: table => new
                {
                    BojaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boje", x => x.BojaID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "Kupci",
                columns: table => new
                {
                    KupacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipKupca = table.Column<int>(type: "int", nullable: false),
                    PIB = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupci", x => x.KupacID);
                });

            migrationBuilder.CreateTable(
                name: "Materijali",
                columns: table => new
                {
                    MaterijalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materijali", x => x.MaterijalID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavci",
                columns: table => new
                {
                    ProdavacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavci", x => x.ProdavacID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodjaci",
                columns: table => new
                {
                    ProizvodjacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodjaci", x => x.ProizvodjacID);
                });

            migrationBuilder.CreateTable(
                name: "PodKategorije",
                columns: table => new
                {
                    PodkategorijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategorijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodKategorije", x => x.PodkategorijaID);
                    table.ForeignKey(
                        name: "FK_PodKategorije_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    PorudzbinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumVreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UkupanIznos = table.Column<double>(type: "float", nullable: false),
                    KupacID = table.Column<int>(type: "int", nullable: false),
                    ProdavacID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.PorudzbinaID);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Kupci_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupci",
                        principalColumn: "KupacID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Prodavci_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavci",
                        principalColumn: "ProdavacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    StanjeNaLageru = table.Column<int>(type: "int", nullable: false),
                    DimenzijeID = table.Column<int>(type: "int", nullable: false),
                    PodkategorijaID = table.Column<int>(type: "int", nullable: false),
                    MaterijalID = table.Column<int>(type: "int", nullable: false),
                    BojaID = table.Column<int>(type: "int", nullable: false),
                    ProizvodjacID = table.Column<int>(type: "int", nullable: false),
                    TipProizvoda = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Punjenje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orijentacija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojMesta = table.Column<int>(type: "int", nullable: true),
                    Rasklopiva = table.Column<bool>(type: "bit", nullable: true),
                    DimenzijaDuseka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImaSanduk = table.Column<bool>(type: "bit", nullable: true),
                    TipKreveta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojVrata = table.Column<int>(type: "int", nullable: true),
                    ImaOgledalo = table.Column<bool>(type: "bit", nullable: true),
                    TipVrata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oblik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sto_BrojMesta = table.Column<int>(type: "int", nullable: true),
                    Rasklopiv = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.ProizvodID);
                    table.ForeignKey(
                        name: "FK_Proizvodi_Boje_BojaID",
                        column: x => x.BojaID,
                        principalTable: "Boje",
                        principalColumn: "BojaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvodi_Materijali_MaterijalID",
                        column: x => x.MaterijalID,
                        principalTable: "Materijali",
                        principalColumn: "MaterijalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvodi_PodKategorije_PodkategorijaID",
                        column: x => x.PodkategorijaID,
                        principalTable: "PodKategorije",
                        principalColumn: "PodkategorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvodi_Proizvodjaci_ProizvodjacID",
                        column: x => x.ProizvodjacID,
                        principalTable: "Proizvodjaci",
                        principalColumn: "ProizvodjacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dostave",
                columns: table => new
                {
                    DostavaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumDostave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CenaDostave = table.Column<double>(type: "float", nullable: false),
                    PorudzbinaID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Dimenzije",
                columns: table => new
                {
                    DimenzijeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sirina = table.Column<double>(type: "float", nullable: false),
                    Visina = table.Column<double>(type: "float", nullable: false),
                    Dubina = table.Column<double>(type: "float", nullable: false),
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimenzije", x => x.DimenzijeID);
                    table.ForeignKey(
                        name: "FK_Dimenzije_Proizvodi_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvodi",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkePorudzbine",
                columns: table => new
                {
                    StavkaPorudzbinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rb = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    CenaPoKomadu = table.Column<double>(type: "float", nullable: false),
                    Iznos = table.Column<double>(type: "float", nullable: false),
                    PorudzbinaID = table.Column<int>(type: "int", nullable: false),
                    ProizvodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkePorudzbine", x => x.StavkaPorudzbinaID);
                    table.ForeignKey(
                        name: "FK_StavkePorudzbine_Porudzbine_PorudzbinaID",
                        column: x => x.PorudzbinaID,
                        principalTable: "Porudzbine",
                        principalColumn: "PorudzbinaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkePorudzbine_Proizvodi_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvodi",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dimenzije_ProizvodID",
                table: "Dimenzije",
                column: "ProizvodID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dostave_PorudzbinaID",
                table: "Dostave",
                column: "PorudzbinaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PodKategorije_KategorijaID",
                table: "PodKategorije",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_KupacID",
                table: "Porudzbine",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_ProdavacID",
                table: "Porudzbine",
                column: "ProdavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_BojaID",
                table: "Proizvodi",
                column: "BojaID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_MaterijalID",
                table: "Proizvodi",
                column: "MaterijalID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_PodkategorijaID",
                table: "Proizvodi",
                column: "PodkategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_ProizvodjacID",
                table: "Proizvodi",
                column: "ProizvodjacID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkePorudzbine_PorudzbinaID",
                table: "StavkePorudzbine",
                column: "PorudzbinaID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkePorudzbine_ProizvodID",
                table: "StavkePorudzbine",
                column: "ProizvodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dimenzije");

            migrationBuilder.DropTable(
                name: "Dostave");

            migrationBuilder.DropTable(
                name: "StavkePorudzbine");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Proizvodi");

            migrationBuilder.DropTable(
                name: "Kupci");

            migrationBuilder.DropTable(
                name: "Prodavci");

            migrationBuilder.DropTable(
                name: "Boje");

            migrationBuilder.DropTable(
                name: "Materijali");

            migrationBuilder.DropTable(
                name: "PodKategorije");

            migrationBuilder.DropTable(
                name: "Proizvodjaci");

            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}

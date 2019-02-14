using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class inicijalno2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "popravka",
                columns: table => new
                {
                    PopravkeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CijenaPopravke = table.Column<double>(nullable: false),
                    DatumPopravke = table.Column<DateTime>(nullable: false),
                    OpisKvara = table.Column<string>(nullable: true),
                    PoslovnicaID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_popravka", x => x.PopravkeId);
                    table.ForeignKey(
                        name: "FK_popravka_poslovnica_PoslovnicaID",
                        column: x => x.PoslovnicaID,
                        principalTable: "poslovnica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_popravka_uposlenik_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "uposlenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "racun",
                columns: table => new
                {
                    RacunId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojRacuna = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    Ukupno = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_racun", x => x.RacunId);
                });

            migrationBuilder.CreateTable(
                name: "stavkeracuna",
                columns: table => new
                {
                    StavkeRacunaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<double>(nullable: false),
                    DioID = table.Column<int>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    PopravkeID = table.Column<int>(nullable: false),
                    RacunID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stavkeracuna", x => x.StavkeRacunaId);
                    table.ForeignKey(
                        name: "FK_stavkeracuna_dio_DioID",
                        column: x => x.DioID,
                        principalTable: "dio",
                        principalColumn: "DioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stavkeracuna_popravka_PopravkeID",
                        column: x => x.PopravkeID,
                        principalTable: "popravka",
                        principalColumn: "PopravkeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stavkeracuna_racun_RacunID",
                        column: x => x.RacunID,
                        principalTable: "racun",
                        principalColumn: "RacunId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stavkeracunaonline",
                columns: table => new
                {
                    StavkeRacunaOnlineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<double>(nullable: false),
                    DioID = table.Column<int>(nullable: false),
                    KlijentID = table.Column<int>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    RacunID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stavkeracunaonline", x => x.StavkeRacunaOnlineId);
                    table.ForeignKey(
                        name: "FK_stavkeracunaonline_dio_DioID",
                        column: x => x.DioID,
                        principalTable: "dio",
                        principalColumn: "DioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stavkeracunaonline_klijent_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stavkeracunaonline_racun_RacunID",
                        column: x => x.RacunID,
                        principalTable: "racun",
                        principalColumn: "RacunId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_popravka_PoslovnicaID",
                table: "popravka",
                column: "PoslovnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_popravka_UposlenikID",
                table: "popravka",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_stavkeracuna_DioID",
                table: "stavkeracuna",
                column: "DioID");

            migrationBuilder.CreateIndex(
                name: "IX_stavkeracuna_PopravkeID",
                table: "stavkeracuna",
                column: "PopravkeID");

            migrationBuilder.CreateIndex(
                name: "IX_stavkeracuna_RacunID",
                table: "stavkeracuna",
                column: "RacunID");

            migrationBuilder.CreateIndex(
                name: "IX_stavkeracunaonline_DioID",
                table: "stavkeracunaonline",
                column: "DioID");

            migrationBuilder.CreateIndex(
                name: "IX_stavkeracunaonline_KlijentID",
                table: "stavkeracunaonline",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_stavkeracunaonline_RacunID",
                table: "stavkeracunaonline",
                column: "RacunID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stavkeracuna");

            migrationBuilder.DropTable(
                name: "stavkeracunaonline");

            migrationBuilder.DropTable(
                name: "popravka");

            migrationBuilder.DropTable(
                name: "racun");
        }
    }
}

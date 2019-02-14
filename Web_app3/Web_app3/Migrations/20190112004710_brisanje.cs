using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class brisanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Macanekes");

            migrationBuilder.DropTable(
                name: "stavkeracunaonline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Macanekes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Slika = table.Column<string>(nullable: true),
                    naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Macanekes", x => x.ID);
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
    }
}

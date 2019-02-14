using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class inicijalno5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_racun_NacinPlacanja_NacinPlacanjaID",
                table: "racun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NacinPlacanja",
                table: "NacinPlacanja");

            migrationBuilder.RenameTable(
                name: "NacinPlacanja",
                newName: "nacinplacanja");

            migrationBuilder.AddPrimaryKey(
                name: "PK_nacinplacanja",
                table: "nacinplacanja",
                column: "NacinPlacanjaId");

            migrationBuilder.CreateTable(
                name: "detaljiprodaje",
                columns: table => new
                {
                    DetaljiProdajeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<double>(nullable: false),
                    DatumProdaje = table.Column<DateTime>(nullable: false),
                    KlijentID = table.Column<int>(nullable: false),
                    NacinPlacanjaID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false),
                    VoziloProdajaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detaljiprodaje", x => x.DetaljiProdajeId);
                    table.ForeignKey(
                        name: "FK_detaljiprodaje_klijent_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detaljiprodaje_nacinplacanja_NacinPlacanjaID",
                        column: x => x.NacinPlacanjaID,
                        principalTable: "nacinplacanja",
                        principalColumn: "NacinPlacanjaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detaljiprodaje_uposlenik_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "uposlenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detaljiprodaje_voziloprodaja_VoziloProdajaID",
                        column: x => x.VoziloProdajaID,
                        principalTable: "voziloprodaja",
                        principalColumn: "VoziloProdajaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vozilopopravka",
                columns: table => new
                {
                    VoziloPopravkaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojMotora = table.Column<string>(nullable: true),
                    BrojSasije = table.Column<string>(nullable: true),
                    GodinaProizvodnje = table.Column<int>(nullable: false),
                    GorivoID = table.Column<int>(nullable: false),
                    KlijentID = table.Column<int>(nullable: false),
                    ModelID = table.Column<int>(nullable: false),
                    PopravkeID = table.Column<int>(nullable: false),
                    RegistracijskaOznaka = table.Column<string>(nullable: true),
                    SnagaMotora = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vozilopopravka", x => x.VoziloPopravkaID);
                    table.ForeignKey(
                        name: "FK_vozilopopravka_gorivo_GorivoID",
                        column: x => x.GorivoID,
                        principalTable: "gorivo",
                        principalColumn: "GorivoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vozilopopravka_klijent_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vozilopopravka_model_ModelID",
                        column: x => x.ModelID,
                        principalTable: "model",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vozilopopravka_popravka_PopravkeID",
                        column: x => x.PopravkeID,
                        principalTable: "popravka",
                        principalColumn: "PopravkeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detaljiprodaje_KlijentID",
                table: "detaljiprodaje",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_detaljiprodaje_NacinPlacanjaID",
                table: "detaljiprodaje",
                column: "NacinPlacanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_detaljiprodaje_UposlenikID",
                table: "detaljiprodaje",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_detaljiprodaje_VoziloProdajaID",
                table: "detaljiprodaje",
                column: "VoziloProdajaID");

            migrationBuilder.CreateIndex(
                name: "IX_vozilopopravka_GorivoID",
                table: "vozilopopravka",
                column: "GorivoID");

            migrationBuilder.CreateIndex(
                name: "IX_vozilopopravka_KlijentID",
                table: "vozilopopravka",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_vozilopopravka_ModelID",
                table: "vozilopopravka",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_vozilopopravka_PopravkeID",
                table: "vozilopopravka",
                column: "PopravkeID");

            migrationBuilder.AddForeignKey(
                name: "FK_racun_nacinplacanja_NacinPlacanjaID",
                table: "racun",
                column: "NacinPlacanjaID",
                principalTable: "nacinplacanja",
                principalColumn: "NacinPlacanjaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_racun_nacinplacanja_NacinPlacanjaID",
                table: "racun");

            migrationBuilder.DropTable(
                name: "detaljiprodaje");

            migrationBuilder.DropTable(
                name: "vozilopopravka");

            migrationBuilder.DropPrimaryKey(
                name: "PK_nacinplacanja",
                table: "nacinplacanja");

            migrationBuilder.RenameTable(
                name: "nacinplacanja",
                newName: "NacinPlacanja");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NacinPlacanja",
                table: "NacinPlacanja",
                column: "NacinPlacanjaId");

            migrationBuilder.AddForeignKey(
                name: "FK_racun_NacinPlacanja_NacinPlacanjaID",
                table: "racun",
                column: "NacinPlacanjaID",
                principalTable: "NacinPlacanja",
                principalColumn: "NacinPlacanjaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

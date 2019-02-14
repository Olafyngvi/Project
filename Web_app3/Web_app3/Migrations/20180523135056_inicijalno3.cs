using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class inicijalno3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brojvrata",
                columns: table => new
                {
                    BrojVrataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brojvrata", x => x.BrojVrataId);
                });

            migrationBuilder.CreateTable(
                name: "gorivo",
                columns: table => new
                {
                    GorivoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gorivo", x => x.GorivoId);
                });

            migrationBuilder.CreateTable(
                name: "oprema",
                columns: table => new
                {
                    OpremaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oprema", x => x.OpremaId);
                });

            migrationBuilder.CreateTable(
                name: "tipvozila",
                columns: table => new
                {
                    TipVozilaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipvozila", x => x.TipVozilaId);
                });

            migrationBuilder.CreateTable(
                name: "transmisija",
                columns: table => new
                {
                    TransmisijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transmisija", x => x.TransmisijaId);
                });

            migrationBuilder.CreateTable(
                name: "voziloprodaja",
                columns: table => new
                {
                    VoziloProdajaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojVrataID = table.Column<int>(nullable: false),
                    Cijena = table.Column<double>(nullable: false),
                    DatumProizvodnje = table.Column<DateTime>(nullable: false),
                    GorivoID = table.Column<int>(nullable: false),
                    Kilometraza = table.Column<string>(nullable: true),
                    Kubikaza = table.Column<string>(nullable: true),
                    ModelID = table.Column<int>(nullable: false),
                    OpremaID = table.Column<int>(nullable: false),
                    SnagaMotora = table.Column<string>(nullable: true),
                    TipVozilaID = table.Column<int>(nullable: false),
                    TransmisijaID = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voziloprodaja", x => x.VoziloProdajaID);
                    table.ForeignKey(
                        name: "FK_voziloprodaja_brojvrata_BrojVrataID",
                        column: x => x.BrojVrataID,
                        principalTable: "brojvrata",
                        principalColumn: "BrojVrataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_voziloprodaja_gorivo_GorivoID",
                        column: x => x.GorivoID,
                        principalTable: "gorivo",
                        principalColumn: "GorivoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_voziloprodaja_model_ModelID",
                        column: x => x.ModelID,
                        principalTable: "model",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_voziloprodaja_oprema_OpremaID",
                        column: x => x.OpremaID,
                        principalTable: "oprema",
                        principalColumn: "OpremaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_voziloprodaja_tipvozila_TipVozilaID",
                        column: x => x.TipVozilaID,
                        principalTable: "tipvozila",
                        principalColumn: "TipVozilaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_voziloprodaja_transmisija_TransmisijaID",
                        column: x => x.TransmisijaID,
                        principalTable: "transmisija",
                        principalColumn: "TransmisijaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vozilaposlovnice",
                columns: table => new
                {
                    VozilaPoslovniceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumUvoza = table.Column<string>(nullable: true),
                    PoslovnicaID = table.Column<int>(nullable: false),
                    VoziloProdajaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vozilaposlovnice", x => x.VozilaPoslovniceId);
                    table.ForeignKey(
                        name: "FK_vozilaposlovnice_poslovnica_PoslovnicaID",
                        column: x => x.PoslovnicaID,
                        principalTable: "poslovnica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vozilaposlovnice_voziloprodaja_VoziloProdajaID",
                        column: x => x.VoziloProdajaID,
                        principalTable: "voziloprodaja",
                        principalColumn: "VoziloProdajaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vozilaposlovnice_PoslovnicaID",
                table: "vozilaposlovnice",
                column: "PoslovnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_vozilaposlovnice_VoziloProdajaID",
                table: "vozilaposlovnice",
                column: "VoziloProdajaID");

            migrationBuilder.CreateIndex(
                name: "IX_voziloprodaja_BrojVrataID",
                table: "voziloprodaja",
                column: "BrojVrataID");

            migrationBuilder.CreateIndex(
                name: "IX_voziloprodaja_GorivoID",
                table: "voziloprodaja",
                column: "GorivoID");

            migrationBuilder.CreateIndex(
                name: "IX_voziloprodaja_ModelID",
                table: "voziloprodaja",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_voziloprodaja_OpremaID",
                table: "voziloprodaja",
                column: "OpremaID");

            migrationBuilder.CreateIndex(
                name: "IX_voziloprodaja_TipVozilaID",
                table: "voziloprodaja",
                column: "TipVozilaID");

            migrationBuilder.CreateIndex(
                name: "IX_voziloprodaja_TransmisijaID",
                table: "voziloprodaja",
                column: "TransmisijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vozilaposlovnice");

            migrationBuilder.DropTable(
                name: "voziloprodaja");

            migrationBuilder.DropTable(
                name: "brojvrata");

            migrationBuilder.DropTable(
                name: "gorivo");

            migrationBuilder.DropTable(
                name: "oprema");

            migrationBuilder.DropTable(
                name: "tipvozila");

            migrationBuilder.DropTable(
                name: "transmisija");
        }
    }
}

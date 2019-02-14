using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class inicijalno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marka",
                columns: table => new
                {
                    MarkaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazvi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marka", x => x.MarkaId);
                });

            migrationBuilder.CreateTable(
                name: "model",
                columns: table => new
                {
                    ModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MarkaID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_model_marka_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "marka",
                        principalColumn: "MarkaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dio",
                columns: table => new
                {
                    DioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<double>(nullable: false),
                    ModelID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Sifra = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dio", x => x.DioId);
                    table.ForeignKey(
                        name: "FK_dio_model_ModelID",
                        column: x => x.ModelID,
                        principalTable: "model",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dio_ModelID",
                table: "dio",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_model_MarkaID",
                table: "model",
                column: "MarkaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dio");

            migrationBuilder.DropTable(
                name: "model");

            migrationBuilder.DropTable(
                name: "marka");
        }
    }
}

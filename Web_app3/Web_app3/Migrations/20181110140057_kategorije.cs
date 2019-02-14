using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class kategorije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "dio",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DioKategorija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true),
                    slikaUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DioKategorija", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dio_KategorijaId",
                table: "dio",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_dio_DioKategorija_KategorijaId",
                table: "dio",
                column: "KategorijaId",
                principalTable: "DioKategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dio_DioKategorija_KategorijaId",
                table: "dio");

            migrationBuilder.DropTable(
                name: "DioKategorija");

            migrationBuilder.DropIndex(
                name: "IX_dio_KategorijaId",
                table: "dio");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "dio");
        }
    }
}

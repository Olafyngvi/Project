using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class inicijalno6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NacinPlacanjaID",
                table: "racun",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NacinPlacanja",
                columns: table => new
                {
                    NacinPlacanjaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrstaPlacanja = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.NacinPlacanjaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_racun_NacinPlacanjaID",
                table: "racun",
                column: "NacinPlacanjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_racun_NacinPlacanja_NacinPlacanjaID",
                table: "racun",
                column: "NacinPlacanjaID",
                principalTable: "NacinPlacanja",
                principalColumn: "NacinPlacanjaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_racun_NacinPlacanja_NacinPlacanjaID",
                table: "racun");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropIndex(
                name: "IX_racun_NacinPlacanjaID",
                table: "racun");

            migrationBuilder.DropColumn(
                name: "NacinPlacanjaID",
                table: "racun");
        }
    }
}

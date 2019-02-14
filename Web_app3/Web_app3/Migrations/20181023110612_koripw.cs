using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class koripw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_osoba_KorisnickoIme",
                table: "osoba");

            migrationBuilder.AlterColumn<string>(
                name: "Lozinka",
                table: "osoba",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickoIme",
                table: "osoba",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_osoba_KorisnickoIme",
                table: "osoba",
                column: "KorisnickoIme",
                unique: true,
                filter: "[KorisnickoIme] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_osoba_KorisnickoIme",
                table: "osoba");

            migrationBuilder.AlterColumn<string>(
                name: "Lozinka",
                table: "osoba",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickoIme",
                table: "osoba",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_osoba_KorisnickoIme",
                table: "osoba",
                column: "KorisnickoIme",
                unique: true);
        }
    }
}

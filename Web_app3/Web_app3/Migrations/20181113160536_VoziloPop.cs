using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class VoziloPop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vozilopopravka_klijent_KlijentID",
                table: "vozilopopravka");

            migrationBuilder.DropIndex(
                name: "IX_vozilopopravka_KlijentID",
                table: "vozilopopravka");

            migrationBuilder.DropColumn(
                name: "BrojMotora",
                table: "vozilopopravka");

            migrationBuilder.DropColumn(
                name: "BrojSasije",
                table: "vozilopopravka");

            migrationBuilder.DropColumn(
                name: "KlijentID",
                table: "vozilopopravka");

            migrationBuilder.DropColumn(
                name: "RegistracijskaOznaka",
                table: "vozilopopravka");

            migrationBuilder.DropColumn(
                name: "SnagaMotora",
                table: "vozilopopravka");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrojMotora",
                table: "vozilopopravka",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojSasije",
                table: "vozilopopravka",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlijentID",
                table: "vozilopopravka",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RegistracijskaOznaka",
                table: "vozilopopravka",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SnagaMotora",
                table: "vozilopopravka",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_vozilopopravka_KlijentID",
                table: "vozilopopravka",
                column: "KlijentID");

            migrationBuilder.AddForeignKey(
                name: "FK_vozilopopravka_klijent_KlijentID",
                table: "vozilopopravka",
                column: "KlijentID",
                principalTable: "klijent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

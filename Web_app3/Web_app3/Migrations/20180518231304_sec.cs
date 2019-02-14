using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klijent_osoba_OsobaId",
                table: "Klijent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klijent",
                table: "Klijent");

            migrationBuilder.DropIndex(
                name: "IX_Klijent_OsobaId",
                table: "Klijent");

            migrationBuilder.RenameTable(
                name: "Klijent",
                newName: "klijent");

            migrationBuilder.AlterColumn<int>(
                name: "OsobaId",
                table: "klijent",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "grad",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_klijent",
                table: "klijent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_klijent_OsobaId",
                table: "klijent",
                column: "OsobaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_klijent_osoba_OsobaId",
                table: "klijent",
                column: "OsobaId",
                principalTable: "osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_klijent_osoba_OsobaId",
                table: "klijent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_klijent",
                table: "klijent");

            migrationBuilder.DropIndex(
                name: "IX_klijent_OsobaId",
                table: "klijent");

            migrationBuilder.RenameTable(
                name: "klijent",
                newName: "Klijent");

            migrationBuilder.AlterColumn<int>(
                name: "OsobaId",
                table: "Klijent",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "grad",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klijent",
                table: "Klijent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Klijent_OsobaId",
                table: "Klijent",
                column: "OsobaId",
                unique: true,
                filter: "[OsobaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Klijent_osoba_OsobaId",
                table: "Klijent",
                column: "OsobaId",
                principalTable: "osoba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class inicijalno4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SifraAutomobila",
                table: "voziloprodaja",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumUvoza",
                table: "vozilaposlovnice",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SifraAutomobila",
                table: "voziloprodaja");

            migrationBuilder.AlterColumn<string>(
                name: "DatumUvoza",
                table: "vozilaposlovnice",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}

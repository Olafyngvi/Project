using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class UpitiVozila3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ime",
                table: "upitivozila");

            migrationBuilder.RenameColumn(
                name: "Prezime",
                table: "upitivozila",
                newName: "ImeiPrezime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImeiPrezime",
                table: "upitivozila",
                newName: "Prezime");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "upitivozila",
                nullable: true);
        }
    }
}

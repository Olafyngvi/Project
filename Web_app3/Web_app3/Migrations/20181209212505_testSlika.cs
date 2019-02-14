using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AutoServis.Migrations
{
    public partial class testSlika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "KontaktUpiti",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "KontaktUpiti",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyNV.Migrations
{
    public partial class DeleteFilePosterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Poster",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}

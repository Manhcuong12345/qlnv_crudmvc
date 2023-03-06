using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyNV.Migrations
{
    public partial class UpdateFieldUploadFileMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Poster",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Users");
        }
    }
}

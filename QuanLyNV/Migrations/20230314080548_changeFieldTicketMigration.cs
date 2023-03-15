using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyNV.Migrations
{
    public partial class changeFieldTicketMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "State",
                table: "Ticket",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

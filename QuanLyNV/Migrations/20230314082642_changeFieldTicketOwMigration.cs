using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyNV.Migrations
{
    public partial class changeFieldTicketOwMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_OwnerId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_OwnerId",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Ticket",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Ticket",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AppUserId",
                table: "Ticket",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_AppUserId",
                table: "Ticket",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_AppUserId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_AppUserId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OwnerId",
                table: "Ticket",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_OwnerId",
                table: "Ticket",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

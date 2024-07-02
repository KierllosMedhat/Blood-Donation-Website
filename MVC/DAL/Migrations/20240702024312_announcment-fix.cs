using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class announcmentfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AspNetUsers_UserId",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Announcements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Hospitals_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Hospitals_UserId",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Announcements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AspNetUsers_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

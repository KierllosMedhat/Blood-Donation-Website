using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class applicationUserKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_AspNetUsers_UserId",
                table: "Hospitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_key",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "key",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_AspNetUsers_UserId",
                table: "Hospitals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_AspNetUsers_UserId",
                table: "Hospitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "key",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_key",
                table: "AspNetUsers",
                column: "key");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_AspNetUsers_UserId",
                table: "Hospitals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

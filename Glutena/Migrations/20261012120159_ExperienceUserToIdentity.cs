using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glutena.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceUserToIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Users_UserId",
                table: "Experiences");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Experiences",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
            migrationBuilder.Sql("UPDATE Experiences SET UserId = NULL");
            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Experiences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Users_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

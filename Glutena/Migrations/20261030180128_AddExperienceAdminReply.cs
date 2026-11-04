using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glutena.Migrations
{
    /// <inheritdoc />
    public partial class AddExperienceAdminReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminReply",
                table: "Experiences",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdminReplyDate",
                table: "Experiences",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminReply",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "AdminReplyDate",
                table: "Experiences");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glutena.Migrations
{
    /// <inheritdoc />
    public partial class AddAffiliateAndSponsorFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffiliateUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSponsored",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SponsorPriority",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffiliateUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsSponsored",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SponsorPriority",
                table: "Products");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class AddedHasBeenApprovedAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenApproved",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenApproved",
                table: "AspNetUsers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class AddedReasonForCancellation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonForCancellation",
                table: "RideRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "ReasonForCancellation",
                table: "RideEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonForCancellation",
                table: "RideEvents");

            migrationBuilder.AddColumn<string>(
                name: "ReasonForCancellation",
                table: "RideRegistrations",
                nullable: true);
        }
    }
}

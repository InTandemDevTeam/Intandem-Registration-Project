using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Migrations
{
    public partial class RemovedIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RideEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RideEvent",
                nullable: false,
                defaultValue: false);
        }
    }
}

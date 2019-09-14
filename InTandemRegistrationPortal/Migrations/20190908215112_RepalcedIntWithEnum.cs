using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Migrations
{
    public partial class RepalcedIntWithEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "RideEvent",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EventType",
                table: "RideEvent",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "RideEvent",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventType",
                table: "RideEvent",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

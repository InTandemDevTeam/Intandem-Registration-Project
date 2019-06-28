using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class UserTypeAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOfUser",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfUser",
                table: "AspNetUsers");
        }
    }
}

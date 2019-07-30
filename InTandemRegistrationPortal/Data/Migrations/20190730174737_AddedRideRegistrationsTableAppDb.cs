using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class AddedRideRegistrationsTableAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RideRegistrations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RideEventsID = table.Column<int>(nullable: false),
                    InTandemUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideRegistrations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RideRegistrations_AspNetUsers_InTandemUserId",
                        column: x => x.InTandemUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RideRegistrations_RideEvents_RideEventsID",
                        column: x => x.RideEventsID,
                        principalTable: "RideEvents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RideRegistrations_InTandemUserId",
                table: "RideRegistrations",
                column: "InTandemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RideRegistrations_RideEventsID",
                table: "RideRegistrations",
                column: "RideEventsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideRegistrations");
        }
    }
}

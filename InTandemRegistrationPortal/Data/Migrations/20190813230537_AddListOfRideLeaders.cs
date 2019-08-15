using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class AddListOfRideLeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagerAssignments");

            migrationBuilder.DropColumn(
                name: "RideLeader",
                table: "RideEvents");

            migrationBuilder.CreateTable(
                name: "RideLeaderAssignments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    EventID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideLeaderAssignments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RideLeaderAssignments_RideEvents_EventID",
                        column: x => x.EventID,
                        principalTable: "RideEvents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RideLeaderAssignments_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RideLeaderAssignments_EventID",
                table: "RideLeaderAssignments",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_RideLeaderAssignments_UserID",
                table: "RideLeaderAssignments",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideLeaderAssignments");

            migrationBuilder.AddColumn<string>(
                name: "RideLeader",
                table: "RideEvents",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ManagerAssignments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerAssignments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ManagerAssignments_RideEvents_EventID",
                        column: x => x.EventID,
                        principalTable: "RideEvents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagerAssignments_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagerAssignments_EventID",
                table: "ManagerAssignments",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerAssignments_UserID",
                table: "ManagerAssignments",
                column: "UserID");
        }
    }
}

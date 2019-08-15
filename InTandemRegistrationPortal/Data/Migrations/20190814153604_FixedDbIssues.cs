using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class FixedDbIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideLeaderAssignments_RideEvents_EventID",
                table: "RideLeaderAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RideLeaderAssignments_AspNetUsers_UserID",
                table: "RideLeaderAssignments");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "RideLeaderAssignments",
                newName: "InTandemUserId");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "RideLeaderAssignments",
                newName: "RideEventsID");

            migrationBuilder.RenameIndex(
                name: "IX_RideLeaderAssignments_UserID",
                table: "RideLeaderAssignments",
                newName: "IX_RideLeaderAssignments_InTandemUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RideLeaderAssignments_EventID",
                table: "RideLeaderAssignments",
                newName: "IX_RideLeaderAssignments_RideEventsID");

            migrationBuilder.AddForeignKey(
                name: "FK_RideLeaderAssignments_AspNetUsers_InTandemUserId",
                table: "RideLeaderAssignments",
                column: "InTandemUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RideLeaderAssignments_RideEvents_RideEventsID",
                table: "RideLeaderAssignments",
                column: "RideEventsID",
                principalTable: "RideEvents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideLeaderAssignments_AspNetUsers_InTandemUserId",
                table: "RideLeaderAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RideLeaderAssignments_RideEvents_RideEventsID",
                table: "RideLeaderAssignments");

            migrationBuilder.RenameColumn(
                name: "RideEventsID",
                table: "RideLeaderAssignments",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "InTandemUserId",
                table: "RideLeaderAssignments",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_RideLeaderAssignments_RideEventsID",
                table: "RideLeaderAssignments",
                newName: "IX_RideLeaderAssignments_EventID");

            migrationBuilder.RenameIndex(
                name: "IX_RideLeaderAssignments_InTandemUserId",
                table: "RideLeaderAssignments",
                newName: "IX_RideLeaderAssignments_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_RideLeaderAssignments_RideEvents_EventID",
                table: "RideLeaderAssignments",
                column: "EventID",
                principalTable: "RideEvents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideLeaderAssignments_AspNetUsers_UserID",
                table: "RideLeaderAssignments",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Data.Migrations
{
    public partial class AddedRideEventsTableAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RideEvents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(type: "varchar(300)", nullable: false),
                    EventSDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    Location = table.Column<string>(type: "varchar(200)", nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(18, 1)", nullable: true),
                    EventType = table.Column<int>(nullable: false),
                    RideLeader = table.Column<string>(type: "varchar(150)", nullable: false),
                    EventRatio = table.Column<string>(type: "varchar(50)", nullable: false),
                    MaxSignup = table.Column<int>(nullable: true),
                    MaxSignUpType = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideEvents", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideEvents");
        }
    }
}

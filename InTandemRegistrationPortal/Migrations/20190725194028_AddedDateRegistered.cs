﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InTandemRegistrationPortal.Migrations
{
    public partial class AddedDateRegistered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistered",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "AspNetUsers");
        }
    }
}

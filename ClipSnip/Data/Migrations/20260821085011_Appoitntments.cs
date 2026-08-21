using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClipSnip.ClipSnip.Data.Migrations
{
    /// <inheritdoc />
    public partial class Appoitntments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfAppointment",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "TimeOfAppointment",
                table: "Appointment");
        }
    }
}

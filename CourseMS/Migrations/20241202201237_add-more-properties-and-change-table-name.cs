using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseMS.Migrations
{
    /// <inheritdoc />
    public partial class addmorepropertiesandchangetablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Courses",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CourseInternalGuid",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseInternalGuid",
                table: "Courses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}

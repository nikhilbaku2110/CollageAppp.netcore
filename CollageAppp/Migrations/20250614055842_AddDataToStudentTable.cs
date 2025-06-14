using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollageAppp.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "india", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "venkat@gmail.com", "venkat" },
                    { 2, "india", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "nikhl@gmail.com", "nikhil" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

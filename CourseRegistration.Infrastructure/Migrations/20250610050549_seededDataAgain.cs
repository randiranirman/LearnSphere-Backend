using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seededDataAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "StudentClassRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "MATH101", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Basic Mathematics", "Mathematics" },
                    { 2, "SCI101", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Basic Science", "Science" },
                    { 3, "HIST101", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "World History", "History" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassRegistrations_StudentId_ClassId",
                table: "StudentClassRegistrations",
                columns: new[] { "StudentId", "ClassId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentClassRegistrations_StudentId_ClassId",
                table: "StudentClassRegistrations");

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "StudentClassRegistrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}

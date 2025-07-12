using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedMOdels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRegistrations_Subjects_SubjectId",
                table: "StudentClassRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassRegistrations_SubjectId",
                table: "StudentClassRegistrations");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentClassRegistrations");

            migrationBuilder.CreateTable(
                name: "StudentRegistrationSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRegistrationId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRegistrationSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRegistrationSubjects_StudentClassRegistrations_StudentRegistrationId",
                        column: x => x.StudentRegistrationId,
                        principalTable: "StudentClassRegistrations",
                        principalColumn: "StudentRegistrationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentRegistrationSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrationSubjects_StudentRegistrationId",
                table: "StudentRegistrationSubjects",
                column: "StudentRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrationSubjects_SubjectId",
                table: "StudentRegistrationSubjects",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentRegistrationSubjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "TeacherSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "StudentClassRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassRegistrations_SubjectId",
                table: "StudentClassRegistrations",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRegistrations_Subjects_SubjectId",
                table: "StudentClassRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectIdToTeacherSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegistrationSubjects_StudentClassRegistrations_StudentRegistrationId",
                table: "StudentRegistrationSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegistrationSubjects_Subjects_SubjectId",
                table: "StudentRegistrationSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentRegistrationSubjects",
                table: "StudentRegistrationSubjects");

            migrationBuilder.RenameTable(
                name: "StudentRegistrationSubjects",
                newName: "StudentRegistrationSubject");

            migrationBuilder.RenameIndex(
                name: "IX_StudentRegistrationSubjects_SubjectId",
                table: "StudentRegistrationSubject",
                newName: "IX_StudentRegistrationSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentRegistrationSubjects_StudentRegistrationId",
                table: "StudentRegistrationSubject",
                newName: "IX_StudentRegistrationSubject_StudentRegistrationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentRegistrationSubject",
                table: "StudentRegistrationSubject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegistrationSubject_StudentClassRegistrations_StudentRegistrationId",
                table: "StudentRegistrationSubject",
                column: "StudentRegistrationId",
                principalTable: "StudentClassRegistrations",
                principalColumn: "StudentRegistrationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegistrationSubject_Subjects_SubjectId",
                table: "StudentRegistrationSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegistrationSubject_StudentClassRegistrations_StudentRegistrationId",
                table: "StudentRegistrationSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegistrationSubject_Subjects_SubjectId",
                table: "StudentRegistrationSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentRegistrationSubject",
                table: "StudentRegistrationSubject");

            migrationBuilder.RenameTable(
                name: "StudentRegistrationSubject",
                newName: "StudentRegistrationSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_StudentRegistrationSubject_SubjectId",
                table: "StudentRegistrationSubjects",
                newName: "IX_StudentRegistrationSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentRegistrationSubject_StudentRegistrationId",
                table: "StudentRegistrationSubjects",
                newName: "IX_StudentRegistrationSubjects_StudentRegistrationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentRegistrationSubjects",
                table: "StudentRegistrationSubjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegistrationSubjects_StudentClassRegistrations_StudentRegistrationId",
                table: "StudentRegistrationSubjects",
                column: "StudentRegistrationId",
                principalTable: "StudentClassRegistrations",
                principalColumn: "StudentRegistrationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegistrationSubjects_Subjects_SubjectId",
                table: "StudentRegistrationSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

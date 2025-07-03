using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Subjects_SubjectId",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRegistration_Class_ClassId",
                table: "StudentClassRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectId",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClassRegistration_Class_ClassId",
                table: "TeacherClassRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClassRegistration_Subjects_SubjectId",
                table: "TeacherClassRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherClassRegistration",
                table: "TeacherClassRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentClassRegistration",
                table: "StudentClassRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Class",
                table: "Class");

            migrationBuilder.RenameTable(
                name: "TeacherClassRegistration",
                newName: "TeacherClassRegistrations");

            migrationBuilder.RenameTable(
                name: "StudentSubject",
                newName: "StudentSubjects");

            migrationBuilder.RenameTable(
                name: "StudentClassRegistration",
                newName: "StudentClassRegistrations");

            migrationBuilder.RenameTable(
                name: "Class",
                newName: "Classes");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherClassRegistration_SubjectId",
                table: "TeacherClassRegistrations",
                newName: "IX_TeacherClassRegistrations_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherClassRegistration_ClassId",
                table: "TeacherClassRegistrations",
                newName: "IX_TeacherClassRegistrations_ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubject_SubjectId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentClassRegistration_ClassId",
                table: "StudentClassRegistrations",
                newName: "IX_StudentClassRegistrations_ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Class_SubjectId",
                table: "Classes",
                newName: "IX_Classes_SubjectId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TeacherClassRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "StudentSubjects",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherClassRegistrations",
                table: "TeacherClassRegistrations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentClassRegistrations",
                table: "StudentClassRegistrations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classes",
                table: "Classes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassRegistrations_TeacherId_ClassId",
                table: "TeacherClassRegistrations",
                columns: new[] { "TeacherId", "ClassId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentId_SubjectId",
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_UniqueAssignment",
                table: "TeacherSubjects",
                columns: new[] { "TeacherID", "SubjectID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectID",
                table: "TeacherSubjects",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRegistrations_Classes_ClassId",
                table: "StudentClassRegistrations",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClassRegistrations_Classes_ClassId",
                table: "TeacherClassRegistrations",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClassRegistrations_Subjects_SubjectId",
                table: "TeacherClassRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRegistrations_Classes_ClassId",
                table: "StudentClassRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClassRegistrations_Classes_ClassId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClassRegistrations_Subjects_SubjectId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherClassRegistrations",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TeacherClassRegistrations_TeacherId_ClassId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentId_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentClassRegistrations",
                table: "StudentClassRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classes",
                table: "Classes");

            migrationBuilder.RenameTable(
                name: "TeacherClassRegistrations",
                newName: "TeacherClassRegistration");

            migrationBuilder.RenameTable(
                name: "StudentSubjects",
                newName: "StudentSubject");

            migrationBuilder.RenameTable(
                name: "StudentClassRegistrations",
                newName: "StudentClassRegistration");

            migrationBuilder.RenameTable(
                name: "Classes",
                newName: "Class");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherClassRegistrations_SubjectId",
                table: "TeacherClassRegistration",
                newName: "IX_TeacherClassRegistration_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherClassRegistrations_ClassId",
                table: "TeacherClassRegistration",
                newName: "IX_TeacherClassRegistration_ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubject",
                newName: "IX_StudentSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentClassRegistrations_ClassId",
                table: "StudentClassRegistration",
                newName: "IX_StudentClassRegistration_ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_SubjectId",
                table: "Class",
                newName: "IX_Class_SubjectId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TeacherClassRegistration",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "StudentSubject",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherClassRegistration",
                table: "TeacherClassRegistration",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentClassRegistration",
                table: "StudentClassRegistration",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Class",
                table: "Class",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Subjects_SubjectId",
                table: "Class",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRegistration_Class_ClassId",
                table: "StudentClassRegistration",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectId",
                table: "StudentSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClassRegistration_Class_ClassId",
                table: "TeacherClassRegistration",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClassRegistration_Subjects_SubjectId",
                table: "TeacherClassRegistration",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}

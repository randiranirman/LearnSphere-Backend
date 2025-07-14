using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedTAbles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClassRegistrations_Subjects_SubjectId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectID",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubject_UniqueAssignment",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherClassRegistrations_TeacherId_ClassId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_Code",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentId_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassRegistrations_StudentId_ClassId",
                table: "StudentClassRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SubjectId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "AssignedAt",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "TeacherID",
                table: "TeacherSubjects",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "TeacherSubjects",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_SubjectID",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_SubjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TeacherClassRegistrations",
                newName: "TeacherRegistrationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subjects",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StudentClassRegistrations",
                newName: "StudentRegistrationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Classes",
                newName: "ClassId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "TeacherSubjects",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "TeacherSubjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByAdminId",
                table: "TeacherSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "TeacherSubjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "TeacherSubjects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "TeacherSubjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TeacherSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "TeacherClassRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredAt",
                table: "TeacherClassRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "TeacherClassRegistrations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrolledAt",
                table: "StudentSubjects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredAt",
                table: "StudentClassRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "IndexNumber",
                table: "StudentClassRegistrations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "StudentClassRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaxStudents",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 30,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "ClassSubjects",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    AssociatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjects", x => new { x.ClassId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ClassSubjects_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassRegistrations_SubjectId",
                table: "StudentClassRegistrations",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_SubjectId",
                table: "ClassSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRegistrations_Subjects_SubjectId",
                table: "StudentClassRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClassRegistrations_Subjects_SubjectId",
                table: "TeacherClassRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRegistrations_Subjects_SubjectId",
                table: "StudentClassRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClassRegistrations_Subjects_SubjectId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectId",
                table: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "ClassSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassRegistrations_SubjectId",
                table: "StudentClassRegistrations");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "ApprovedByAdminId",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TeacherClassRegistrations");

            migrationBuilder.DropColumn(
                name: "IndexNumber",
                table: "StudentClassRegistrations");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentClassRegistrations");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TeacherSubjects",
                newName: "TeacherID");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "TeacherSubjects",
                newName: "SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_SubjectID");

            migrationBuilder.RenameColumn(
                name: "TeacherRegistrationId",
                table: "TeacherClassRegistrations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Subjects",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StudentRegistrationId",
                table: "StudentClassRegistrations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Classes",
                newName: "Id");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "TeacherSubjects",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                table: "TeacherSubjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "TeacherClassRegistrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredAt",
                table: "TeacherClassRegistrations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Subjects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrolledAt",
                table: "StudentSubjects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredAt",
                table: "StudentClassRegistrations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MaxStudents",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 30);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_TeacherSubject_UniqueAssignment",
                table: "TeacherSubjects",
                columns: new[] { "TeacherID", "SubjectID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassRegistrations_TeacherId_ClassId",
                table: "TeacherClassRegistrations",
                columns: new[] { "TeacherId", "ClassId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Code",
                table: "Subjects",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentId_SubjectId",
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassRegistrations_StudentId_ClassId",
                table: "StudentClassRegistrations",
                columns: new[] { "StudentId", "ClassId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectId",
                table: "Classes",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes",
                column: "SubjectId",
                principalTable: "Subjects",
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
                name: "FK_TeacherClassRegistrations_Subjects_SubjectId",
                table: "TeacherClassRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectID",
                table: "TeacherSubjects",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

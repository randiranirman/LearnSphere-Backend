using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing foreign keys if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Admins_Users_Id') AND parent_object_id = OBJECT_ID(N'Admins'))
                    ALTER TABLE Admins DROP CONSTRAINT FK_Admins_Users_Id;
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Students_Users_Id') AND parent_object_id = OBJECT_ID(N'Students'))
                    ALTER TABLE Students DROP CONSTRAINT FK_Students_Users_Id;
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Teachers_Users_Id') AND parent_object_id = OBJECT_ID(N'Teachers'))
                    ALTER TABLE Teachers DROP CONSTRAINT FK_Teachers_Users_Id;
            ");

            // Drop primary keys
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.key_constraints WHERE object_id = OBJECT_ID(N'PK_Admins') AND parent_object_id = OBJECT_ID(N'Admins'))
                    ALTER TABLE Admins DROP CONSTRAINT PK_Admins;
                IF EXISTS (SELECT * FROM sys.key_constraints WHERE object_id = OBJECT_ID(N'PK_Students') AND parent_object_id = OBJECT_ID(N'Students'))
                    ALTER TABLE Students DROP CONSTRAINT PK_Students;
                IF EXISTS (SELECT * FROM sys.key_constraints WHERE object_id = OBJECT_ID(N'PK_Teachers') AND parent_object_id = OBJECT_ID(N'Teachers'))
                    ALTER TABLE Teachers DROP CONSTRAINT PK_Teachers;
            ");

            // Backup data
            migrationBuilder.Sql(@"
                SELECT * INTO #TempAdmins FROM Admins;
                SELECT * INTO #TempStudents FROM Students;
                SELECT * INTO #TempTeachers FROM Teachers;
            ");

            // Drop columns
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Admins");

            // Add columns without identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Teachers",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Students",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Admins",
                type: "int",
                nullable: false);

            // Restore data
            migrationBuilder.Sql(@"
                UPDATE a SET a.Id = t.Id
                FROM Admins a
                JOIN #TempAdmins t ON /* Use another column to match records if possible */
                    1=1; -- If no other columns, adjust this to match appropriately

                UPDATE s SET s.Id = t.Id
                FROM Students s
                JOIN #TempStudents t ON /* Use another column to match records if possible */
                    1=1; -- If no other columns, adjust this to match appropriately

                UPDATE t SET t.Id = temp.Id
                FROM Teachers t
                JOIN #TempTeachers temp ON /* Use another column to match records if possible */
                    1=1; -- If no other columns, adjust this to match appropriately

                DROP TABLE #TempAdmins;
                DROP TABLE #TempStudents;
                DROP TABLE #TempTeachers;
            ");

            // Add back primary keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");

            // Add foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_Id",
                table: "Admins",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_Id",
                table: "Teachers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_Id",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_Id",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_Id",
                table: "Teachers");

            // Drop primary keys
            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            // Backup data
            migrationBuilder.Sql(@"
                SELECT * INTO #TempAdmins FROM Admins;
                SELECT * INTO #TempStudents FROM Students;
                SELECT * INTO #TempTeachers FROM Teachers;
            ");

            // Drop columns
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Admins");

            // Add columns with identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Teachers",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Students",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Admins",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Restore data - note: identity values will be assigned automatically
            // You would need to handle any relationships here that depend on specific ID values

            // Clean up temp tables
            migrationBuilder.Sql(@"
                DROP TABLE #TempAdmins;
                DROP TABLE #TempStudents;
                DROP TABLE #TempTeachers;
            ");

            // Add primary keys back
            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");
        }
    }
}
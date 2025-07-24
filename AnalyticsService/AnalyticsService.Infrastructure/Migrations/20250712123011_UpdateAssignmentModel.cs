using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the foreign key constraint exists before dropping it
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Assignments_Students_StudentId')
                    ALTER TABLE [Assignments] DROP CONSTRAINT [FK_Assignments_Students_StudentId];
            ");

            // Check if the index exists before dropping it
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Assignments_StudentId' AND object_id = OBJECT_ID('Assignments'))
                    DROP INDEX [IX_Assignments_StudentId] ON [Assignments];
            ");

            // Check if the column exists before dropping it
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.columns WHERE name = 'StudentId' AND object_id = OBJECT_ID('Assignments'))
                    ALTER TABLE [Assignments] DROP COLUMN [StudentId];
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_StudentId",
                table: "Assignments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}

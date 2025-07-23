using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTeacherSubjectSubjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Migration left empty as the column and index already exist
            // This migration was created to fix conflicts but is no longer needed
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Migration rollback left empty as this migration doesn't perform any operations
        }
    }
}

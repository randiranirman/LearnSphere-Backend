using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStorage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changetestdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_SubjectTopics_SubjectTopicId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTopics",
                table: "SubjectTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_SubjectTopicId",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "SubjectTopicId",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "SubjectTopics",
                newName: "SubjectTopic");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTopic",
                table: "SubjectTopic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_AssignmentId",
                table: "Submission",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_TopicId",
                table: "Material",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_SubjectTopic_TopicId",
                table: "Material",
                column: "TopicId",
                principalTable: "SubjectTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_SubjectTopic_TopicId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Submission_AssignmentId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTopic",
                table: "SubjectTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_TopicId",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "SubjectTopic",
                newName: "SubjectTopics");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "SubjectTopicId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTopics",
                table: "SubjectTopics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                column: "AssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubjectTopicId",
                table: "Materials",
                column: "SubjectTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_SubjectTopics_SubjectTopicId",
                table: "Materials",
                column: "SubjectTopicId",
                principalTable: "SubjectTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

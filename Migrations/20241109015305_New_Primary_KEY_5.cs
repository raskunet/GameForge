using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New_Primary_KEY_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Answer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies",
                columns: new[] { "UserID", "ThreadTopicID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                columns: new[] { "QuestionID", "UserID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "Answer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies",
                columns: new[] { "AuthorID", "ThreadTopicID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                columns: new[] { "QuestionID", "AuthorID" });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies",
                column: "UserID");
        }
    }
}

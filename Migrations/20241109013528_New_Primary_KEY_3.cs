using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New_Primary_KEY_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_AuthorID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_AuthorID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_AuthorID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AuthorID",
                table: "Answer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies",
                columns: new[] { "AuthorID", "ThreadTopicID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                columns: new[] { "QuestionID", "AuthorID" });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_ThreadTopicID",
                table: "ThreadTopicReplies",
                column: "ThreadTopicID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionID",
                table: "Answer",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID",
                table: "ThreadTopicReplies",
                column: "ThreadTopicID",
                principalTable: "ThreadTopic",
                principalColumn: "ThreadTopicID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ThreadTopicID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies",
                columns: new[] { "ThreadTopicID", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                columns: new[] { "QuestionID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_AuthorID",
                table: "ThreadTopicReplies",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AuthorID",
                table: "Answer",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_AuthorID",
                table: "Answer",
                column: "AuthorID",
                principalTable: "Question",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_AuthorID",
                table: "ThreadTopicReplies",
                column: "AuthorID",
                principalTable: "ThreadTopic",
                principalColumn: "ThreadTopicID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

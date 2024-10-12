using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class CreateModelForums10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopic",
                table: "ThreadTopic");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadTopicID",
                table: "ThreadTopic",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopic",
                table: "ThreadTopic",
                columns: new[] { "ThreadTopicID", "UserID" });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "integer", nullable: false),
                    AuthorID = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    Upvotes = table.Column<int>(type: "integer", nullable: false),
                    Downvotes = table.Column<int>(type: "integer", nullable: false),
                    LatestAnswerID = table.Column<int>(type: "integer", nullable: false),
                    LatestAnswerTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfAnswers = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => new { x.QuestionID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_Question_User_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false),
                    Upvotes = table.Column<int>(type: "integer", nullable: false),
                    Downvotes = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => new { x.QuestionID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Answer_Question_UserID_QuestionID",
                        columns: x => new { x.UserID, x.QuestionID },
                        principalTable: "Question",
                        principalColumns: new[] { "QuestionID", "AuthorID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answer_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserID_QuestionID",
                table: "Answer",
                columns: new[] { "UserID", "QuestionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_AuthorID",
                table: "Question",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID_UserID",
                table: "ThreadTopicReplies",
                columns: new[] { "ThreadTopicID", "UserID" },
                principalTable: "ThreadTopic",
                principalColumns: new[] { "ThreadTopicID", "UserID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_User_UserID",
                table: "ThreadTopicReplies",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_User_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopic",
                table: "ThreadTopic");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadTopicID",
                table: "ThreadTopic",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopic",
                table: "ThreadTopic",
                column: "ThreadTopicID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID",
                table: "ThreadTopicReplies",
                column: "ThreadTopicID",
                principalTable: "ThreadTopic",
                principalColumn: "ThreadTopicID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

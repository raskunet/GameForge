using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New_Primary_KEY_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_UserID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_User_UserID1",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopic",
                table: "ThreadTopic");

            migrationBuilder.RenameColumn(
                name: "UserID1",
                table: "Answer",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_UserID1",
                table: "Answer",
                newName: "IX_Answer_AuthorID");

            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_AuthorID",
                table: "ThreadTopicReplies",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_AuthorID",
                table: "Answer",
                column: "AuthorID",
                principalTable: "Question",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_User_UserID",
                table: "Answer",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_AuthorID",
                table: "ThreadTopicReplies",
                column: "AuthorID",
                principalTable: "ThreadTopic",
                principalColumn: "ThreadTopicID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_AuthorID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_User_UserID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_AuthorID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_AuthorID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopic",
                table: "ThreadTopic");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "ThreadTopicReplies");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Answer",
                newName: "UserID1");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_AuthorID",
                table: "Answer",
                newName: "IX_Answer_UserID1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_UserID",
                table: "Answer",
                column: "UserID",
                principalTable: "Question",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_User_UserID1",
                table: "Answer",
                column: "UserID1",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID_UserID",
                table: "ThreadTopicReplies",
                columns: new[] { "ThreadTopicID", "UserID" },
                principalTable: "ThreadTopic",
                principalColumns: new[] { "ThreadTopicID", "UserID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

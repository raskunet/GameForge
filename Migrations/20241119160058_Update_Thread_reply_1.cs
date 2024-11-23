using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class Update_Thread_reply_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentThreadReplyThreadTopicID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentThreadReplyUserID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreadTopicReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_ParentThreadReplyUserID_ParentThreadRepl~",
                table: "ThreadTopicReplies",
                columns: new[] { "ParentThreadReplyUserID", "ParentThreadReplyThreadTopicID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentThreadReplyUser~",
                table: "ThreadTopicReplies",
                columns: new[] { "ParentThreadReplyUserID", "ParentThreadReplyThreadTopicID" },
                principalTable: "ThreadTopicReplies",
                principalColumns: new[] { "UserID", "ThreadTopicID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentThreadReplyUser~",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentThreadReplyUserID_ParentThreadRepl~",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "ParentThreadReplyThreadTopicID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "ParentThreadReplyUserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "ThreadTopicReplyID",
                table: "ThreadTopicReplies");
        }
    }
}

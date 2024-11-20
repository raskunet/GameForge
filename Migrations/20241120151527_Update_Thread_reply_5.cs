using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class Update_Thread_reply_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentThreadReplyThre~",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentThreadReplyThreadTopicReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "ParentThreadReplyThreadTopicReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID",
                principalTable: "ThreadTopicReplies",
                principalColumn: "ThreadTopicReplyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.AddColumn<int>(
                name: "ParentThreadReplyThreadTopicReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_ParentThreadReplyThreadTopicReplyID",
                table: "ThreadTopicReplies",
                column: "ParentThreadReplyThreadTopicReplyID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentThreadReplyThre~",
                table: "ThreadTopicReplies",
                column: "ParentThreadReplyThreadTopicReplyID",
                principalTable: "ThreadTopicReplies",
                principalColumn: "ThreadTopicReplyID");
        }
    }
}

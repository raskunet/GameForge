using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class Update_Thread_reply_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_User_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies",
                column: "UserID");

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
                name: "FK_ThreadTopicReplies_User_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_User_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class Update_Thread_reply_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.AlterColumn<int>(
                name: "ParentReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID",
                principalTable: "ThreadTopicReplies",
                principalColumn: "ThreadTopicReplyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.AlterColumn<int>(
                name: "ParentReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID",
                principalTable: "ThreadTopicReplies",
                principalColumn: "ThreadTopicReplyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

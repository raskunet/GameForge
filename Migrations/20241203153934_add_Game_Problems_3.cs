using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class add_Game_Problems_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_AspNetUsers_UserId",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ThreadTopicReplies",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ThreadTopicReplies_UserId",
                table: "ThreadTopicReplies",
                newName: "IX_ThreadTopicReplies_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "ThreadTopicReplies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_AspNetUsers_UserID",
                table: "ThreadTopicReplies",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_AspNetUsers_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ThreadTopicReplies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ThreadTopicReplies_UserID",
                table: "ThreadTopicReplies",
                newName: "IX_ThreadTopicReplies_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ThreadTopicReplies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_AspNetUsers_UserId",
                table: "ThreadTopicReplies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

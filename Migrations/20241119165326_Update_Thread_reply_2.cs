using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class Update_Thread_reply_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentThreadReplyUser~",
                table: "ThreadTopicReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_User_UserID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentThreadReplyUserID_ParentThreadRepl~",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "ParentThreadReplyThreadTopicID",
                table: "ThreadTopicReplies");

            migrationBuilder.RenameColumn(
                name: "ParentThreadReplyUserID",
                table: "ThreadTopicReplies",
                newName: "ParentThreadReplyThreadTopicReplyID");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadTopicReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies",
                column: "ThreadTopicReplyID");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_User_ParentReplyID",
                table: "ThreadTopicReplies",
                column: "ParentReplyID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopicReplies_ParentThreadReplyThre~",
                table: "ThreadTopicReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_User_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopicReplies_ParentThreadReplyThreadTopicReplyID",
                table: "ThreadTopicReplies");

            migrationBuilder.RenameColumn(
                name: "ParentThreadReplyThreadTopicReplyID",
                table: "ThreadTopicReplies",
                newName: "ParentThreadReplyUserID");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadTopicReplyID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ParentThreadReplyThreadTopicID",
                table: "ThreadTopicReplies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadTopicReplies",
                table: "ThreadTopicReplies",
                columns: new[] { "UserID", "ThreadTopicID" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_User_UserID",
                table: "ThreadTopicReplies",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

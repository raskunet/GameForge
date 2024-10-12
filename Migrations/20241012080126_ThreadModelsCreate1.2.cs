using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class ThreadModelsCreate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ThreadTopic",
                newName: "ThreadTopicID");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTopic_UserID",
                table: "ThreadTopic",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopic_User_UserID",
                table: "ThreadTopic",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopic_User_UserID",
                table: "ThreadTopic");

            migrationBuilder.DropIndex(
                name: "IX_ThreadTopic_UserID",
                table: "ThreadTopic");

            migrationBuilder.RenameColumn(
                name: "ThreadTopicID",
                table: "ThreadTopic",
                newName: "ID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class ThreadModelsCreate11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID",
                table: "ThreadTopicReplies",
                column: "ThreadTopicID",
                principalTable: "ThreadTopic",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadTopicReplies_ThreadTopic_ThreadTopicID",
                table: "ThreadTopicReplies");
        }
    }
}

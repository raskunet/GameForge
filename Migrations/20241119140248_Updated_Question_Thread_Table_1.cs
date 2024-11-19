using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Question_Thread_Table_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormatTitle",
                table: "ThreadTopic");

            migrationBuilder.DropColumn(
                name: "FormatTitle",
                table: "Question");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormatTitle",
                table: "ThreadTopic",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormatTitle",
                table: "Question",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

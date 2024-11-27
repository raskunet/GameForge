using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New_fields_in_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditTime",
                table: "ThreadTopicReplies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditTime",
                table: "ThreadTopic",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditTime",
                table: "Question",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEditTime",
                table: "ThreadTopicReplies");

            migrationBuilder.DropColumn(
                name: "LastEditTime",
                table: "ThreadTopic");

            migrationBuilder.DropColumn(
                name: "LastEditTime",
                table: "Question");
        }
    }
}

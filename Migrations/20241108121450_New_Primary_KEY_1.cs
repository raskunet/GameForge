using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New_Primary_KEY_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_UserID_QuestionID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_User_UserID",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserID_QuestionID",
                table: "Answer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionID",
                table: "Question",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Answer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserID",
                table: "Answer",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserID1",
                table: "Answer",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_UserID",
                table: "Answer",
                column: "UserID",
                principalTable: "Question",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_User_UserID1",
                table: "Answer",
                column: "UserID1",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_UserID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_User_UserID1",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserID",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserID1",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Answer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionID",
                table: "Question",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                columns: new[] { "QuestionID", "AuthorID" });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserID_QuestionID",
                table: "Answer",
                columns: new[] { "UserID", "QuestionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_UserID_QuestionID",
                table: "Answer",
                columns: new[] { "UserID", "QuestionID" },
                principalTable: "Question",
                principalColumns: new[] { "QuestionID", "AuthorID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_User_UserID",
                table: "Answer",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

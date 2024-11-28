using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_Game_GameId",
                table: "Library");

            migrationBuilder.DropForeignKey(
                name: "FK_Library_User_UserID",
                table: "Library");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library",
                table: "Library");

            migrationBuilder.RenameTable(
                name: "Library",
                newName: "Libraries");

            migrationBuilder.RenameIndex(
                name: "IX_Library_UserID",
                table: "Libraries",
                newName: "IX_Libraries_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Library_GameId",
                table: "Libraries",
                newName: "IX_Libraries_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries",
                column: "LibraryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Game_GameId",
                table: "Libraries",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_User_UserID",
                table: "Libraries",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Game_GameId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_User_UserID",
                table: "Libraries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries");

            migrationBuilder.RenameTable(
                name: "Libraries",
                newName: "Library");

            migrationBuilder.RenameIndex(
                name: "IX_Libraries_UserID",
                table: "Library",
                newName: "IX_Library_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Libraries_GameId",
                table: "Library",
                newName: "IX_Library_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library",
                table: "Library",
                column: "LibraryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Game_GameId",
                table: "Library",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Library_User_UserID",
                table: "Library",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

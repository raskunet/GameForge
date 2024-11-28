using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Collectables_CollectablesCollectableID_CollectablesUse~",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_CollectablesCollectableID_CollectablesUserID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CollectablesCollectableID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CollectablesUserID",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "LibraryID",
                table: "Collectables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCollectables",
                table: "Collectables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collectables_LibraryID",
                table: "Collectables",
                column: "LibraryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectables_Libraries_LibraryID",
                table: "Collectables",
                column: "LibraryID",
                principalTable: "Libraries",
                principalColumn: "LibraryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectables_Libraries_LibraryID",
                table: "Collectables");

            migrationBuilder.DropIndex(
                name: "IX_Collectables_LibraryID",
                table: "Collectables");

            migrationBuilder.DropColumn(
                name: "LibraryID",
                table: "Collectables");

            migrationBuilder.DropColumn(
                name: "TotalCollectables",
                table: "Collectables");

            migrationBuilder.AddColumn<int>(
                name: "CollectablesCollectableID",
                table: "Game",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectablesUserID",
                table: "Game",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_CollectablesCollectableID_CollectablesUserID",
                table: "Game",
                columns: new[] { "CollectablesCollectableID", "CollectablesUserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Collectables_CollectablesCollectableID_CollectablesUse~",
                table: "Game",
                columns: new[] { "CollectablesCollectableID", "CollectablesUserID" },
                principalTable: "Collectables",
                principalColumns: new[] { "CollectableID", "UserID" });
        }
    }
}

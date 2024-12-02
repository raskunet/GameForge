using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class New4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Wishlist_WishlistID_WishlistUserID",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_WishlistID_WishlistUserID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "WishlistID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "WishlistUserID",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Wishlist",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WishlistAdditionDate",
                table: "Wishlist",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_GameId",
                table: "Wishlist",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_UserID",
                table: "Wishlist",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Game_GameId",
                table: "Wishlist",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_User_UserID",
                table: "Wishlist",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Game_GameId",
                table: "Wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_User_UserID",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_GameId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_UserID",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "WishlistAdditionDate",
                table: "Wishlist");

            migrationBuilder.AddColumn<int>(
                name: "WishlistID",
                table: "Game",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WishlistUserID",
                table: "Game",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_WishlistID_WishlistUserID",
                table: "Game",
                columns: new[] { "WishlistID", "WishlistUserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Wishlist_WishlistID_WishlistUserID",
                table: "Game",
                columns: new[] { "WishlistID", "WishlistUserID" },
                principalTable: "Wishlist",
                principalColumns: new[] { "WishlistID", "UserID" });
        }
    }
}

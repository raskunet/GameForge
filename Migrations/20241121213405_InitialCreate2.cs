using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameForge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCheckedOut = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.CartID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Cart_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collectables",
                columns: table => new
                {
                    CollectableID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectables", x => new { x.CollectableID, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    LibraryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    LibraryCreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => new { x.LibraryID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Library_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    WishlistID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => new { x.WishlistID, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameName = table.Column<string>(type: "text", nullable: false),
                    Developer = table.Column<string>(type: "text", nullable: false),
                    Publisher = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: true),
                    CartID = table.Column<int>(type: "integer", nullable: true),
                    CartUserID = table.Column<int>(type: "integer", nullable: true),
                    CollectablesCollectableID = table.Column<int>(type: "integer", nullable: true),
                    CollectablesUserID = table.Column<int>(type: "integer", nullable: true),
                    LibraryID = table.Column<int>(type: "integer", nullable: true),
                    LibraryUserID = table.Column<int>(type: "integer", nullable: true),
                    WishlistID = table.Column<int>(type: "integer", nullable: true),
                    WishlistUserID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Game_Cart_CartID_CartUserID",
                        columns: x => new { x.CartID, x.CartUserID },
                        principalTable: "Cart",
                        principalColumns: new[] { "CartID", "UserID" });
                    table.ForeignKey(
                        name: "FK_Game_Collectables_CollectablesCollectableID_CollectablesUse~",
                        columns: x => new { x.CollectablesCollectableID, x.CollectablesUserID },
                        principalTable: "Collectables",
                        principalColumns: new[] { "CollectableID", "UserID" });
                    table.ForeignKey(
                        name: "FK_Game_Library_LibraryID_LibraryUserID",
                        columns: x => new { x.LibraryID, x.LibraryUserID },
                        principalTable: "Library",
                        principalColumns: new[] { "LibraryID", "UserID" });
                    table.ForeignKey(
                        name: "FK_Game_Wishlist_WishlistID_WishlistUserID",
                        columns: x => new { x.WishlistID, x.WishlistUserID },
                        principalTable: "Wishlist",
                        principalColumns: new[] { "WishlistID", "UserID" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CartID_CartUserID",
                table: "Game",
                columns: new[] { "CartID", "CartUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Game_CollectablesCollectableID_CollectablesUserID",
                table: "Game",
                columns: new[] { "CollectablesCollectableID", "CollectablesUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Game_LibraryID_LibraryUserID",
                table: "Game",
                columns: new[] { "LibraryID", "LibraryUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Game_WishlistID_WishlistUserID",
                table: "Game",
                columns: new[] { "WishlistID", "WishlistUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Library_UserID",
                table: "Library",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Collectables");

            migrationBuilder.DropTable(
                name: "Library");

            migrationBuilder.DropTable(
                name: "Wishlist");
        }
    }
}

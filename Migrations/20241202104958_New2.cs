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
                name: "FK_Collectables_Libraries_LibraryID",
                table: "Collectables");

            migrationBuilder.DropIndex(
                name: "IX_Collectables_LibraryID",
                table: "Collectables");

            migrationBuilder.DropColumn(
                name: "LibraryID",
                table: "Collectables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryID",
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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroGame.Migrations
{
    public partial class AddGameModelforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperId",
                table: "Game",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Game");
        }
    }
}

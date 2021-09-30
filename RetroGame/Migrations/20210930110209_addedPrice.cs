using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroGame.Migrations
{
    public partial class addedPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangeInPrice",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompletePrice",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeInPrice",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CompletePrice",
                table: "Game");
        }
    }
}

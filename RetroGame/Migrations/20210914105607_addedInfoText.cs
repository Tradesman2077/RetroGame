using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroGame.Migrations
{
    public partial class addedInfoText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfoText",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoText",
                table: "Game");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRay.Migrations
{
    public partial class RealMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserGames");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserGames",
                nullable: true);
        }
    }
}

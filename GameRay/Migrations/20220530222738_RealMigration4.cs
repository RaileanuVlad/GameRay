using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRay.Migrations
{
    public partial class RealMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BanLink",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgLink",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanLink",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ImgLink",
                table: "Games");
        }
    }
}

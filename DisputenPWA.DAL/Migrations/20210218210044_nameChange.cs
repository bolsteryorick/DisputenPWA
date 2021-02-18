using Microsoft.EntityFrameworkCore.Migrations;

namespace DisputenPWA.DAL.Migrations
{
    public partial class nameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrowserId",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "AppInstanceId",
                table: "RefreshTokens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppInstanceId",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "BrowserId",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

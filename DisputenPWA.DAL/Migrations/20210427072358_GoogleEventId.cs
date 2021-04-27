using Microsoft.EntityFrameworkCore.Migrations;

namespace DisputenPWA.DAL.Migrations
{
    public partial class GoogleEventId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleEventId",
                table: "AppEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleEventId",
                table: "AppEvents");
        }
    }
}

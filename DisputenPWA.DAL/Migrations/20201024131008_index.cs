using Microsoft.EntityFrameworkCore.Migrations;

namespace DisputenPWA.DAL.Migrations
{
    public partial class index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppEvents_StartTime",
                table: "AppEvents",
                column: "StartTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppEvents_StartTime",
                table: "AppEvents");
        }
    }
}

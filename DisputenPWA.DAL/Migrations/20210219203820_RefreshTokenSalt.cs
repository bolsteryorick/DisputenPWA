using Microsoft.EntityFrameworkCore.Migrations;

namespace DisputenPWA.DAL.Migrations
{
    public partial class RefreshTokenSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AppInstanceId",
                table: "RefreshTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefrehTokenSalt",
                table: "RefreshTokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppInstanceId",
                table: "RefreshTokens",
                column: "AppInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AppInstanceId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "RefrehTokenSalt",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "AppInstanceId",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

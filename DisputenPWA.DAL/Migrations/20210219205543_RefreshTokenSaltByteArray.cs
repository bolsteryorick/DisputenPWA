using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisputenPWA.DAL.Migrations
{
    public partial class RefreshTokenSaltByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefrehTokenSalt",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<byte[]>(
                name: "RefreshTokenSalt",
                table: "RefreshTokens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenSalt",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "RefrehTokenSalt",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

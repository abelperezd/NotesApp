using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Data
{
    /// <inheritdoc />
    public partial class seedDatabaseAgainAndAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "PasswordHashset", "RegisterDate", "UserName" },
                values: new object[] { 4, "lolito@hotmail.com", "Manolo", "f7g8", new DateOnly(2013, 9, 15), "manolito" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

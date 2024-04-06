using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notes.Data
{
    /// <inheritdoc />
    public partial class dbPopulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Note",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Name", "RegisterDate", "UserName" },
                values: new object[,]
                {
                    { 1, "Abel", new DateOnly(2010, 10, 4), "abel99" },
                    { 2, "David", new DateOnly(2011, 11, 7), "david00" },
                    { 3, "Maria", new DateOnly(2012, 8, 14), "mar.ia15" }
                });

            migrationBuilder.InsertData(
                table: "Note",
                columns: new[] { "Id", "CreationDate", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2010, 10, 4, 10, 30, 0, 0, DateTimeKind.Unspecified), "First note", 1 },
                    { 2, new DateTime(2010, 10, 5, 15, 32, 1, 0, DateTimeKind.Unspecified), "Second note!", 1 },
                    { 3, new DateTime(2011, 8, 3, 21, 16, 2, 0, DateTimeKind.Unspecified), "Second note!", 3 }
                });

            migrationBuilder.InsertData(
                table: "NoteLike",
                columns: new[] { "Id", "NoteId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                table: "User");

            migrationBuilder.DeleteData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NoteLike",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NoteLike",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NoteLike",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NoteLike",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Note",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}

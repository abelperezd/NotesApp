using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Data
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Third note!");

            migrationBuilder.CreateIndex(
                name: "IX_Note_NoteImportanceId",
                table: "Note",
                column: "NoteImportanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_NoteImportance_NoteImportanceId",
                table: "Note",
                column: "NoteImportanceId",
                principalTable: "NoteImportance",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_NoteImportance_NoteImportanceId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_NoteImportanceId",
                table: "Note");

            migrationBuilder.UpdateData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Second note!");
        }
    }
}

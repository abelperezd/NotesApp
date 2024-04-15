using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notes.Data.Migrations
{
    /// <inheritdoc />
    public partial class noteImportance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteImportanceId",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NoteImportance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Importance = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteImportance", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 1,
                column: "NoteImportanceId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 2,
                column: "NoteImportanceId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 3,
                column: "NoteImportanceId",
                value: 3);

            migrationBuilder.InsertData(
                table: "NoteImportance",
                columns: new[] { "Id", "Importance" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteImportance");

            migrationBuilder.DropColumn(
                name: "NoteImportanceId",
                table: "Note");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HallOfTodos.API.Migrations
{
    public partial class TodoNoteAddWrittenByColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WrittenBy",
                table: "TodoNotes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WrittenBy",
                table: "TodoNotes");
        }
    }
}

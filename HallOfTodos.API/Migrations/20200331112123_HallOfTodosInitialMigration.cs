using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HallOfTodos.API.Migrations
{
    public partial class HallOfTodosInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Todo = table.Column<string>(maxLength: 50, nullable: false),
                    Doing = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Complete = table.Column<bool>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: true),
                    CompleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Details = table.Column<string>(maxLength: 200, nullable: true),
                    TodoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoNotes_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoNotes_TodoId",
                table: "TodoNotes",
                column: "TodoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoNotes");

            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteTable",
                columns: table => new
                {
                    NotesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Reminder = table.Column<DateTime>(nullable: false),
                    Colour = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsArchive = table.Column<bool>(nullable: false),
                    IsPin = table.Column<bool>(nullable: false),
                    IsTrash = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTable", x => x.NotesId);
                    table.ForeignKey(
                        name: "FK_NoteTable_UserTable_Id",
                        column: x => x.Id,
                        principalTable: "UserTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTable_Id",
                table: "NoteTable",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteTable");
        }
    }
}

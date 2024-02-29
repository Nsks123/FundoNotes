using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class fee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedBackTable",
                columns: table => new
                {
                    feedback_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    feedback_date = table.Column<string>(nullable: true),
                    feedback_message = table.Column<string>(nullable: true),
                    rating = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBackTable", x => x.feedback_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedBackTable");
        }
    }
}

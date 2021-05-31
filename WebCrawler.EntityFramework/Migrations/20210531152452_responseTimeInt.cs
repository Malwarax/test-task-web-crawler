using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.EntityFramework.Migrations
{
    public partial class responseTimeInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponseTimeInt",
                table: "PerformanceResults",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseTimeInt",
                table: "PerformanceResults");
        }
    }
}

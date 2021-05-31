using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.EntityFramework.Migrations
{
    public partial class renameResponseTimeInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseTimeInt",
                table: "PerformanceResults");

            migrationBuilder.AddColumn<int>(
                name: "ResponseTime",
                table: "PerformanceResults",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseTime",
                table: "PerformanceResults");

            migrationBuilder.AddColumn<int>(
                name: "ResponseTimeInt",
                table: "PerformanceResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

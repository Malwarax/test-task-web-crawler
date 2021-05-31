using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.EntityFramework.Migrations
{
    public partial class tests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceResults_Websites_WebsiteId",
                table: "PerformanceResults");

            migrationBuilder.DropTable(
                name: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceResults_WebsiteId",
                table: "PerformanceResults");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "PerformanceResults");

            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "PerformanceResults");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "PerformanceResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "PerformanceResults",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceResults_TestId",
                table: "PerformanceResults",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceResults_Tests_TestId",
                table: "PerformanceResults",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceResults_Tests_TestId",
                table: "PerformanceResults");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceResults_TestId",
                table: "PerformanceResults");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "PerformanceResults");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "PerformanceResults");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "PerformanceResults",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "PerformanceResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteLink = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceResults_WebsiteId",
                table: "PerformanceResults",
                column: "WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceResults_Websites_WebsiteId",
                table: "PerformanceResults",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

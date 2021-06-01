using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.EntityFramework.Migrations
{
    public partial class uniqueLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnlySitemapUrls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 1024, nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlySitemapUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlySitemapUrls_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlyWebsiteUrls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 1024, nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlyWebsiteUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlyWebsiteUrls_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlySitemapUrls_TestId",
                table: "OnlySitemapUrls",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlyWebsiteUrls_TestId",
                table: "OnlyWebsiteUrls",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlySitemapUrls");

            migrationBuilder.DropTable(
                name: "OnlyWebsiteUrls");
        }
    }
}

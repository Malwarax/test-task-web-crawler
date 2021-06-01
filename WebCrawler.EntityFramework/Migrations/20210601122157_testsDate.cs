using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.EntityFramework.Migrations
{
    public partial class testsDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Tests",
                nullable: true,
                defaultValueSql: "GETUTCDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Tests");
        }
    }
}

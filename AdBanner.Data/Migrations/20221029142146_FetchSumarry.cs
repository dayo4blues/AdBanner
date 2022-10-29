using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdBanner.Data.Migrations
{
    public partial class FetchSumarry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerStatsSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClickPerHour = table.Column<int>(type: "int", nullable: false),
                    ImpressionPerHour = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerStatsSummaries");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class AddsLanguageCultureColumnToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageCulture",
                table: "Sections",
                nullable: true,
                defaultValue: "en");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCulture",
                table: "ContentTags",
                nullable: true,
                defaultValue: "en");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCulture",
                table: "Articles",
                nullable: true,
                defaultValue: "en");

            migrationBuilder.Sql(@"USE Havana500
UPDATE Articles
SET LanguageCulture = 'en'

UPDATE Sections
SET LanguageCulture = 'en'

UPDATE ContentTags
SET LanguageCulture = 'en'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCulture",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "LanguageCulture",
                table: "ContentTags");

            migrationBuilder.DropColumn(
                name: "LanguageCulture",
                table: "Articles");
        }
    }
}

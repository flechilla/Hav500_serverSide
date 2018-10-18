using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class AddColumnsToPictureAndPivotTableWithTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "PIctures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCulture",
                table: "PIctures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PIctures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PictureContentTag",
                columns: table => new
                {
                    PictureId = table.Column<int>(nullable: false),
                    ContentTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureContentTag", x => new { x.PictureId, x.ContentTagId });
                    table.ForeignKey(
                        name: "FK_PictureContentTag_ContentTags_ContentTagId",
                        column: x => x.ContentTagId,
                        principalTable: "ContentTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PictureContentTag_PIctures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "PIctures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PictureContentTag_ContentTagId",
                table: "PictureContentTag",
                column: "ContentTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureContentTag");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "PIctures");

            migrationBuilder.DropColumn(
                name: "LanguageCulture",
                table: "PIctures");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PIctures");
        }
    }
}

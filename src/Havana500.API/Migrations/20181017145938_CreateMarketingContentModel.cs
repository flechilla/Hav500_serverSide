using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class CreateMarketingContentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketingContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    ContentLevel = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    PictureId = table.Column<int>(nullable: true),
                    LanguageCulture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketingContents_PIctures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "PIctures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketingContentTags",
                columns: table => new
                {
                    MarketingContentId = table.Column<int>(nullable: false),
                    ContentTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingContentTags", x => new { x.MarketingContentId, x.ContentTagId });
                    table.ForeignKey(
                        name: "FK_MarketingContentTags_ContentTags_ContentTagId",
                        column: x => x.ContentTagId,
                        principalTable: "ContentTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketingContentTags_MarketingContents_MarketingContentId",
                        column: x => x.MarketingContentId,
                        principalTable: "MarketingContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketingContents_PictureId",
                table: "MarketingContents",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingContentTags_ContentTagId",
                table: "MarketingContentTags",
                column: "ContentTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketingContentTags");

            migrationBuilder.DropTable(
                name: "MarketingContents");
        }
    }
}

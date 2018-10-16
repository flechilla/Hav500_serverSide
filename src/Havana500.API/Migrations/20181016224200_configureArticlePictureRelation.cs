using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class configureArticlePictureRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PIctures_Articles_ArticleId",
                table: "PIctures");

            migrationBuilder.DropIndex(
                name: "IX_PIctures_ArticleId",
                table: "PIctures");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "PIctures");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "PIctures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PIctures_ArticleId",
                table: "PIctures",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PIctures_Articles_ArticleId",
                table: "PIctures",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class AddRelativePathColumnToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelativePath",
                table: "PIctures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelativePath",
                table: "PIctures");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class AddColumnWeightToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "PIctures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PIctures");
        }
    }
}

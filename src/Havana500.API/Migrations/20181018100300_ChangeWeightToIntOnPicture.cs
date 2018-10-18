using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class ChangeWeightToIntOnPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "PIctures",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "PIctures",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

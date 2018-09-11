using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class ModifyTablesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticlesContentTags",
                table: "ArticlesContentTags");

            migrationBuilder.DropColumn(
                name: "ParentDiscriminator",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ArticlesContentTags");

            migrationBuilder.RenameTable(
                name: "ArticlesContentTags",
                newName: "ArticleContentTag");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfArticles",
                table: "Sections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sections",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Views",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateUtc",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateUtc",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "EditorWeight",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReadingTime",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Articles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleContentTag",
                table: "ArticleContentTag",
                columns: new[] { "ArticleId", "ContentTagId" });

            migrationBuilder.CreateTable(
                name: "ContentTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AmountOfContent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleContentTag_ContentTagId",
                table: "ArticleContentTag",
                column: "ContentTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContentTag_Articles_ArticleId",
                table: "ArticleContentTag",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContentTag_ContentTags_ContentTagId",
                table: "ArticleContentTag",
                column: "ContentTagId",
                principalTable: "ContentTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContentTag_Articles_ArticleId",
                table: "ArticleContentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContentTag_ContentTags_ContentTagId",
                table: "ArticleContentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "ContentTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleContentTag",
                table: "ArticleContentTag");

            migrationBuilder.DropIndex(
                name: "IX_ArticleContentTag_ContentTagId",
                table: "ArticleContentTag");

            migrationBuilder.DropColumn(
                name: "AmountOfArticles",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "EditorWeight",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ReadingTime",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "ArticleContentTag",
                newName: "ArticlesContentTags");

            migrationBuilder.AlterColumn<long>(
                name: "ArticleId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ParentDiscriminator",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Views",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateUtc",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateUtc",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ArticlesContentTags",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticlesContentTags",
                table: "ArticlesContentTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

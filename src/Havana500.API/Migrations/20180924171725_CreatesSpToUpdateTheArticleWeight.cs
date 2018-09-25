using Microsoft.EntityFrameworkCore.Migrations;

namespace Havana500.Migrations
{
    public partial class CreatesSpToUpdateTheArticleWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE Havana500;
GO
-- =============================================
-- Author:		Adriano Flechilla
-- Email: a.flechilla91@gmail.com
-- Create date: 24/9/2018
-- Description:	This trigger increase the amountOfComments in the related Article table.
-- =============================================
IF OBJECT_ID('update_article_amount_of_comments') IS NOT NULL BEGIN
	DROP TRIGGER  update_article_amount_of_comments
END
GO

CREATE TRIGGER update_article_amount_of_comments 
   ON  Comments
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @articleId INT;
	SELECT @articleId = ArticleId FROM inserted;
	IF (SELECT ArticleId FROM inserted) IS NOT NULL BEGIN
		UPDATE Articles
		SET AmountOfComments = AmountOfComments + 1
		WHERE Id = @articleId	

		DECLARE @SectionId INT;
		SELECT @SectionId = A.SectionId 
		FROM Articles AS A 
		WHERE A.Id = @articleId

		PRINT  @sectionId

		UPDATE Sections
		SET AmountOfComments = AmountOfComments + 1
		WHERE Id = @SectionId
	END	
END
GO

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER update_article_amount_of_comments;");
        }
    }
}

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE Havana500;
GO
-- =============================================
-- Author:		Adriano Flechilla
-- Email: a.flechilla91@gmail.com
-- Create date: 25/9/2018
-- Description:	This trigger increase the amountOfContent in the related ContentTags table.
-- =============================================
IF OBJECT_ID('update_contentTag_amount_of_content') IS NOT NULL BEGIN
	DROP TRIGGER  update_contentTag_amount_of_content
END
GO

CREATE TRIGGER update_contentTag_amount_of_content 
   ON  ArticleContentTag
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @contentTagId INT;
	SELECT @contentTagId = contentTagId FROM inserted;
		
		UPDATE ContentTags
		SET AmountOfContent = AmountOfContent + 1
		WHERE Id = @contentTagId			
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE Havana500;
GO
-- =============================================
-- Author: Adriano Flechilla
-- Email: a.flechilla91@gmail.com
-- Create date: 24/9/2018
-- Description:	This trigger increase the amountOfViews in the Sections table.
-- =============================================
IF OBJECT_ID('update_articles_view') IS NOT NULL BEGIN
	DROP TRIGGER  update_articles_view
END
GO
CREATE TRIGGER update_articles_view
   ON  Articles
   AFTER UPDATE
   AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--TODO: Get the modified column, if is the Views, then run the query
    -- Insert statements for trigger here
	SELECT *
	FROM 
END
GO

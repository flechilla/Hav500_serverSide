SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adriano Flechilla
-- Email: a.flechilla91@gmail.com
-- Create date: 24/9/2018
-- Description:	This SP is used to update the column Weight of Articles. 
-- =============================================
USE [Havana500];
GO

IF OBJECT_ID('usp_updateArticlesWeight') IS NOT NULL BEGIN
	DROP PROC usp_updateArticlesWeight;
END
GO

CREATE PROC dbo.usp_updateArticlesWeight AS
	DECLARE @max_weight REAL;

	UPDATE [dbo].[Articles]
	SET Weight = ((Views + 1.1) * (AmountOfComments + 1.1) * (EditorWeight)) / (DATEDIFF(DAY, StartDateUtc, GETDATE())*10)

	SELECT TOP 1 @max_weight = A.Weight
	FROM [dbo].[Articles] AS A
	ORDER BY A.Weight DESC;

	UPDATE [dbo].[Articles]
	SET Weight =  (Weight / @max_weight) * 100;  

	RETURN 0;
GO

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

CREATE TABLE dbo.Articles_Table_Changes (SectionId int, ArticleId INT);
GO

CREATE TRIGGER update_articles_view
   ON  Articles
   AFTER UPDATE
   AS 
BEGIN
 SET NOCOUNT ON;
    --Unpivot deleted
    WITH deleted_unpvt AS (
        SELECT SectionId, Id
        FROM 
           (SELECT SectionId, Id, FieldName, FieldValue, Views
           FROM deleted) p
        UNPIVOT
           (FieldValue FOR FieldName IN (Views)
        ) AS deleted_unpvt
    ),
    --Unpivot inserted
    inserted_unpvt AS (
        SELECT SectionId, Id
        FROM 
           (SELECT SectionId, Id, FieldName, FieldValue, Views
           FROM inserted) p
        UNPIVOT
           (FieldValue FOR FieldName IN 
              (Age)
        ) AS inserted_unpvt
    )

    --Join them together and show what's changed
    INSERT INTO Articles_Table_Changes (SectionId, Id)
    SELECT Coalesce (D.SectionId, I.SectionId) SectionId, 
	Coalesce (D.Id, I.Id) Id
    FROM 
        deleted_unpvt d
            FULL OUTER JOIN 
        inserted_unpvt i
            on      D.SectionId = I.SectionId 
                --AND D.FieldName = I.FieldName
    WHERE
         D.FieldValue <> I.FieldValue --Changes
		-- AND D.FieldName = 'Age'
        --OR (D.FieldValue IS NOT NULL AND I.FieldValue IS NULL) -- Deletions
        --OR (D.FieldValue IS NULL AND I.FieldValue IS NOT NULL) -- Insertions
END
GO

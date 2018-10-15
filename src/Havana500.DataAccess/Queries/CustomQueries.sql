USE Havana500;

--GET Article's comments with pagination----
--SELECT C.*
--FROM Articles AS A
--INNER JOIN Comments AS C
--ON A.Id = C.ArticleId
--WHERE A.Id = 6
--ORDER BY C.CreatedAt DESC
--OFFSET 0 ROWS
--FETCH NEXT 10 ROWS ONLY
-----------------------------------------

--GET articles with its related tags------------
--FROM Articles AS A
--INNER JOIN ArticleContentTag AS ACT
--ON A.Id = ACT.ArticleId
--INNER JOIN ContentTags AS CT
--ON ACT.ContentTagId = CT.ID
--WHERE A.Id = 34
--GROUP BY A.Id, A.Title, A.Body, A.ReadingTime, A.StartDateUtc, A.AllowComments, A.AllowAnonymousComments, A.MetaDescription, A.MetaKeywords, A.MetaTitle, A.Views,  CT.Name 
--------------------------

-- SELECT CT.Id, CT.Name
--FROM ArticleContentTag AS ACT
--INNER JOIN ContentTags AS CT
--ON ACT.ContentTagId = CT.ID
--WHERE ACT.ArticleId =	6

----------CLEAN TABLES-----------
--DELETE FROM ContentTags
--DELETE FROM Sections


--SELECT TOP(10) A.Id, A.Title, A.Views, A.AmountOfComments, A.ApprovedCommentCount, A.NotApprovedCommentCount
--FROM Articles AS A
--ORDER BY A.Views ASC



--SELECT A.Id AS ArticleId, A.Title, A.StartDateUtc,
--COUNT(C.Id) AS AmountOfNewCommentsComments
--FROM Articles AS A
--INNER JOIN Comments AS C
--ON A.Id = C.ArticleId
--WHERE DATEDIFF(DAY, C.CreatedAt, GETDATE()) <= 500
--GROUP BY A.Id,A.Title, A.StartDateUtc
--HAVING COUNT(C.Id)>0
--ORDER BY COUNT(C.Id) DESC

--DECLARE @Date DATE = '09-23-2018'; 

--SELECT DATEDIFF( DAY, @Date, GETDATE())

--UPDATE Articles
--SET StartDateUtc = @Date
--WHERE Id = 713

--UPDATE Articles 
--SET Weight = ((Views + 1.1) * (AmountOfComments + 1.1) * (EditorWeight+1.1) + .001) / (DATEDIFF(DAY, StartDateUtc, GETDATE())*10)

--DECLARE @max_weight REAL;

--SELECT TOP 1 @max_weight = A.Weight
--FROM Articles AS A
--ORDER BY A.Weight DESC

--PRINT @max_weight

--UPDATE Articles
--SET Weight =  (Weight / @max_weight) * 100


--SELECT A.Id, A.Weight,A.EditorWeight, A.StartDateUtc, A.AmountOfComments, A.Views, DATEDIFF(DAY, StartDateUtc, GETDATE()) AS DateDiff,  ((Views + 1.1) * (AmountOfComments + 1.1) * (EditorWeight+1.1) + .001) AS Numerator
--FROM Articles AS A
--ORDER BY Weight DESC

------ declaration of the SP for updating the Weight of the Articles --------
 
--EXEC dbo.usp_updateArticlesWeight;

--SELECT A.Id, A.Weight,A.EditorWeight, A.StartDateUtc, A.AmountOfComments, A.Views, DATEDIFF(DAY, StartDateUtc, GETDATE()) AS DateDiff,  ((Views + 1.1) * (AmountOfComments + 1.1) * (EditorWeight+1.1) + .001) AS Numerator
--FROM Articles AS A
--ORDER BY Weight DESC

--SELECT S.AmountOfComments AS SectionAmountOfComments, 
--	   A.AmountOfComments AS ArticlesAmountOfComments, 
--	   C.Id AS CommentId, A.Id AS ArticleId, C.Body AS CommentBody
--FROM Comments AS C
--INNER JOIN Articles AS A ON A.Id = C.ArticleId
--INNER JOIN Sections AS S ON A.SectionId = S.Id
--ORDER BY C.Id DESC

--INSERT INTO ArticleContentTag
--VALUES(757, 150) 

--SELECT Id, AmountOfContent
--FROm ContentTags AS CT
--ORDER BY Id DESC




-- GET RELATED ARTICLES--

--SELECT A.Id AS ArticleId, CT.Name as TagName, A.LanguageCulture
--FROM Articles As A
--INNER JOIN ArticleContentTag as ACT ON A.Id = ACT.ArticleId
--INNER JOIN ContentTags as CT ON CT.Id = ACT.ContentTagId
----WHERE 
--GROUP BY A.Id, CT.Name, A.LanguageCulture
--ORDER BY A.Id

-- -------------------- Setup tables and some initial data --------------------
--CREATE TABLE dbo.Sample_Table (ContactID int, Forename varchar(100), Surname varchar(100), Extn varchar(16), Email varchar(100), Age int );
--INSERT INTO Sample_Table VALUES (1,'Bob','Smith','2295','bs@example.com',24);
--INSERT INTO Sample_Table VALUES (2,'Alice','Brown','2255','ab@example.com',32);
--INSERT INTO Sample_Table VALUES (3,'Reg','Jones','2280','rj@example.com',19);
--INSERT INTO Sample_Table VALUES (4,'Mary','Doe','2216','md@example.com',28);
--INSERT INTO Sample_Table VALUES (5,'Peter','Nash','2214','pn@example.com',25);

--CREATE TABLE dbo.Sample_Table_Changes (ContactID int, FieldName sysname, FieldValueWas INT, FieldValueIs INT, modified datetime default (GETDATE()));

--GO

---- -------------------- Create trigger --------------------
--CREATE TRIGGER TriggerName ON dbo.Sample_Table FOR UPDATE AS
--BEGIN
--    SET NOCOUNT ON;
--    --Unpivot deleted
--    WITH deleted_unpvt AS (
--        SELECT ContactID, FieldName, FieldValue
--        FROM 
--           (SELECT ContactID, Age
--           FROM deleted) p
--        UNPIVOT
--           (FieldValue FOR FieldName IN 
--              (Age)
--        ) AS deleted_unpvt
--    ),
--    --Unpivot inserted
--    inserted_unpvt AS (
--        SELECT ContactID, FieldName, FieldValue
--        FROM 
--           (SELECT ContactID,  Age
--           FROM inserted) p
--        UNPIVOT
--           (FieldValue FOR FieldName IN 
--              (Age)
--        ) AS inserted_unpvt
--    )

--    --Join them together and show what's changed
--    INSERT INTO Sample_Table_Changes (ContactID, FieldName, FieldValueWas, FieldValueIs)
--    SELECT Coalesce (D.ContactID, I.ContactID) ContactID
--        , Coalesce (D.FieldName, I.FieldName) FieldName
--        , D.FieldValue as FieldValueWas
--        , I.FieldValue AS FieldValueIs 
--    FROM 
--        deleted_unpvt d

--            FULL OUTER JOIN 
--        inserted_unpvt i
--            on      D.ContactID = I.ContactID 
--                AND D.FieldName = I.FieldName
--    WHERE
--         D.FieldValue <> I.FieldValue --Changes
--		-- AND D.FieldName = 'Age'
--        --OR (D.FieldValue IS NOT NULL AND I.FieldValue IS NULL) -- Deletions
--        --OR (D.FieldValue IS NULL AND I.FieldValue IS NOT NULL) -- Insertions
--END
--GO
---- -------------------- Try some changes --------------------
--UPDATE Sample_Table SET age = age+1;
--UPDATE Sample_Table SET Extn = '5'+Extn where Extn Like '221_';

--DELETE FROM Sample_Table WHERE ContactID = 3;

--INSERT INTO Sample_Table VALUES (6,'Stephen','Turner','2299','st@example.com',25);

--UPDATE Sample_Table SET ContactID = 7 where ContactID = 4; --this will be shown as a delete and an insert
---- -------------------- See the results --------------------
--SELECT *, SQL_VARIANT_PROPERTY(FieldValueWas, 'BaseType') FieldBaseType, SQL_VARIANT_PROPERTY(FieldValueWas, 'MaxLength') FieldMaxLength from Sample_Table_Changes;

---- -------------------- Cleanup --------------------
--DROP TABLE dbo.Sample_Table; DROP TABLE dbo.Sample_Table_Changes;


--SELECT A.Title, SUBSTRING(A.Body, 0, 100)+'...' AS Body, A.Views, A.ApprovedCommentCount, A.StartDateUtc
--FROm Articles A
--INNER JOIN Sections As S ON S.Id = A.SectionId
--WHERE s.Name = 'Cine'
--ORDER BY A.Weight DESC
--OFFSET 0 ROWS
--FETCH NEXT 10 ROWS ONLY


--SELECT * FROM Comments WHERE ArticleId = 589 ORDER BY CreatedAt DESC

--SELECT P.RelativePath, P.FullPath
--FROM Articles AS A
--INNER JOIN PIctures As P ON p.ArticleId = a.id

----GET ARTICLE SUMMARY By SECTION NAME-----
--WITH articleMainImage AS
--(
--	SELECT    P.RelativePath, P.SeoFilename, P.MimeType, p.PictureType, P.Id, P.ArticleId
--	FROM Pictures AS P
--	WHERE P.PictureType = 2
--)
--SELECT A.Title, SUBSTRING(A.Body, 0, 100)+'...' AS Body, S.Name,
--	   A.Views, A.ApprovedCommentCount, A.StartDateUtc, A.Id,
--	   P.RelativePath, P.SeoFilename, P.MimeType, p.PictureType
--FROm Articles A
--INNER JOIN Sections As S ON S.Id = A.SectionId
--LEFT JOIN articleMainImage AS P ON P.ArticleId = A.Id
--WHERE s.Name = 'cine'
--ORDER BY A.Weight DESC
--OFFSET 0 ROWS
--FETCH NEXT 10 ROWS ONLY

--GET ARTICLE SUMMARY By SECTION NAME AND TAGS IDS-----
WITH articleMainImage AS
(
	SELECT    P.RelativePath, P.SeoFilename, P.MimeType, p.PictureType, P.Id, P.ArticleId
	FROM Pictures AS P
	WHERE P.PictureType = 2
)
SELECT A.Title, SUBSTRING(A.Body, 0, 100)+'...' AS Body, S.Name,
	   A.Views, A.ApprovedCommentCount, A.StartDateUtc, A.Id,
	   P.RelativePath, P.SeoFilename, P.MimeType, p.PictureType
FROm Articles A
INNER JOIN Sections As S ON S.Id = A.SectionId
LEFT JOIN articleMainImage AS P ON P.ArticleId = A.Id
INNER JOIN ArticleContentTag AS ACT ON A.Id = ACT.ArticleId
WHERE s.Name = 'cine' AND ACT.ContentTagId IN (121)
ORDER BY A.Weight DESC


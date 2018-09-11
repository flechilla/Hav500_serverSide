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







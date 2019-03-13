USE Havana500;

SELECT *
FROM Sections

SELECT *
FROM Articles   

UPDATE Articles 
SET SectionId = 4
WHERE SectionId IN (5,6,7,8)

UPDATE Articles 
SET SectionId = 1
WHERE Id BETWEEN 1 AND 10
 
UPDATE Articles 
SET SectionId = 2
WHERE Id BETWEEN 11 AND 20

UPDATE Articles 
SET SectionId = 6
WHERE Id BETWEEN 31 AND 40

update sections
set Name = 'Impacto'
where Id = 5

update sections
set Name = 'Curiosidades'
where Id = 6
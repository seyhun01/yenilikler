SELECT * FROM Models

CREATE TABLE ModeltoComputer (
 ModelId int references Models(Id),
 ComputerId int references Computers(Id)

)
CREATE VIEW MODLIST
AS
Select m.Country 'olke' ,c.Name 'ad' from Computers c
JOIN ModeltoComputer mc
ON
c.Id=mc.ComputerId
INNER JOIN Models m
ON
mc.ModelId=m.Id
CREATE procedure spComList
as
begin
Select m.Name 'Model adi' ,c.Name ' Computer adi' from Computers c
JOIN ModeltoComputer mc
ON
c.Id=mc.ComputerId
INNER JOIN Models m
ON
mc.ModelId=m.Id
end
spComList




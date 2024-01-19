SELECT * FROM Models

CREATE TABLE ModeltoComputer (
 ModelId int references Models(Id),
 ComputerId int references Computers(Id)

)
--CREATE VIEW MODLIST
AS
Select m.Country 'olke' ,c.Name 'ad' from Computers c
JOIN ModeltoComputer mc
ON
c.Id=mc.ComputerId
INNER JOIN Models m
ON
mc.ModelId=m.Id
--CREATE procedure spComList
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

-- second 

CREATE TABLE CarNames(
Id int primary key identity,
Name nvarchar(50),
Country nvarchar(50)
)
Create table Typecars (
Id int primary key identity,
Name nvarchar(50),
Year int ,
CarId int references Carnames(Id)
)
CREATE procedure spFullcarnames
AS
begin
select* from Typecars tc
inner join CarNames cs
on 
cs.Id=tc.CarId
end
create view CarCountry
as
select cs.Country 'olke', tc.Name 'marka' from Typecars tc
inner join CarNames cs
on 
cs.Id=tc.CarId
select * from CarCountry
--  third
create table Footballer (
Id int primary key identity , 
Name nvarchar(50) ,
Age int ,
Team nvarchar(50)
)
create view vwTeam
as
select f.Name 'ad', f.Team 'komanda' from Footballer f
Where  f.Age=35




create procedure spAge
as 
begin
select f.name 'ad' , f.Age 'yas' from Football f
end













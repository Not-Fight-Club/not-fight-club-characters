--DDL for P2Group2SocialGame
--Still need to add the on delete cascades and set nulls

--CREATE DATABASE P3_NotFightClub_Characters
--Go

USE P3_NotFightClub_Characters
Go

DROP TABLE IF EXISTS [Character];
DROP TABLE IF EXISTS Trait;
DROP TABLE IF EXISTS Weapon;

CREATE TABLE Trait(
TraitId int not null identity(1,1) primary key,
[Description] nvarchar(300)
)

CREATE TABLE Weapon(
WeaponId int not null identity(1,1) primary key,
[Description] nvarchar(300)
)

CREATE TABLE [Character](
CharacterId int not null identity (1,1) Primary Key,
[Name] nvarchar(100) not null,
[Level] int,
Wins int,
Losses int,
Ties int,
Baseform nvarchar(100),
UserId uniqueidentifier not null,
TraitId int not null FOREIGN KEY REFERENCES Trait(TraitId) on delete no action ,
WeaponId int not null FOREIGN KEY REFERENCES Weapon(WeaponId) on delete no action
)


create database ShoppingSite

use ShoppingSite






--create table Role(

--Id int primary key identity(1,1),
--Name nvarchar (100) not null

--);

--create table UserTbl(

--Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
--UserName nvarchar(100) not null,
--UserRole int foreign key references Role(Id) not null,
--Email nvarchar(254) not null,
--PasswordHash nvarchar(100) NOT NULL,
--CreatedAt datetime default Current_timestamp,


--);

------------------------ Identity tables ---------------------------------------------

drop table AppUser

Create table AppUser(
Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
UserName nvarchar(256)  null,
NormalizedUserName nvarchar(256),
Email nvarchar(256),
NormalizedEmail nvarchar(256),
EmailConfirmed bit not null,
PasswordHash nvarchar(max),
SecurityStamp nvarchar(max),
ConcurrencyStamp nvarchar(max),
PhoneNumber nvarchar(max),
PhoneNumberConfirmed bit ,
TwoFactorEnabled bit not null ,
LockoutEnd datetimeoffset(7),
LockoutEnabled bit not null,
AcessFailedCount int not null
);

alter table AppUser
add AccessFailedCount int not null



create table Roles(

Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
Name nvarchar(256),
NormalizedName nvarchar(256),
ConcurrencyStamp nvarchar(max)
);

drop table UserRoles

create table UserRoles(

UserId UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
RoleId Uniqueidentifier foreign key references Roles(Id) not null,
PRIMARY KEY (UserId, RoleId)

);

--drop table UserLogins

Create table UserLogins (

LoginProvider uniqueIdentifier  not null,
ProviderKey uniqueIdentifier not null,
ProviderDisplayName nvarchar(max),
UserId UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
PRIMARY KEY (LoginProvider, ProviderKey)

);

--drop table UserTokens

Create table UserTokens (

UserId UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
LoginProvider nvarchar(450)  not null,
Name nvarchar(450) not null ,
Value nvarchar(max),
PRIMARY KEY (UserId, LoginProvider,Name)


);

Create table RoleClaims (
Id int primary key not null,
RoleId uniqueIdentifier foreign key references Roles(Id) not null, 
ClaimType nvarchar(max) ,
ClaimValue nvarchar(max)

);

create table UserClaims(

Id int primary key not null,
UserId UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
ClaimType nvarchar(max),
ClaimValue nvarchar(max)

);









-------------------------------------------------------------------------------------------


CREATE TABLE Brand (
    Id int primary key identity(1,1),
    Name nvarchar(100) not null,
	CreatedAt datetime default Current_timestamp not null,
	ModifiedAt datetime ,
	CreatedBy UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
	ModifiedBy UNIQUEIDENTIFIER foreign key references AppUser(Id) 


);

drop table Category

create table Category(
	Id int primary key identity(1,1),
	Name nvarchar(100) not null,
	Created_At datetime default Current_timestamp not null,
	ModifiedAt datetime ,
	CreatedBy UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
	ModifiedBy UNIQUEIDENTIFIER foreign key references AppUser(Id) 

);





create table product(

Id int primary key identity(1,1),
Title  nvarchar (100) not null,
Description nvarchar (500) not null,
BrandId int not null foreign key references  Brand(Id),
CategoryId int  not null foreign key references Category(Id),
Price decimal(8,2) not null ,
Discount decimal(8,2) ,
StockDate datetime not null,
NoOfStock int not null ,
ExpiryDate datetime not null,
CreatedAt datetime default Current_timestamp not null,
ModifiedAt datetime ,
CreatedBy UNIQUEIDENTIFIER foreign key references AppUser(Id) not null,
ModifiedBy UNIQUEIDENTIFIER foreign key references AppUser(Id) 
);


create table Image(

Id int primary key identity(100,1),
ProductId int foreign key references Product(Id) not null,
Version int not null ,

Image nvarchar(300) not null,

UniqueImageName nvarchar(500) default Current_timestamp not null



);


drop table Image


----get image SP


create procedure Get_Product_by_ID (@ProductId int)

As
Begin try

Select Top 1 Id,ProductId,Version,Image,UniqueImageName from Image
where ProductId = @ProductId
Order By Version Desc


End try
BEGIN CATCH
-- REPORTING THE EXCEPTION
SELECT ERROR_NUMBER() AS ERRORNO,ERROR_LINE() AS ERRORLINE,ERROR_MESSAGE() AS ERRORMSG,
ERROR_PROCEDURE() AS ERRORPROCEDURE,ERROR_SEVERITY() AS ERRORSEVERITY
END CATCH;
Go;

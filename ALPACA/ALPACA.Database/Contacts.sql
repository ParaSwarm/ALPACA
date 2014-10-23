CREATE TABLE [dbo].[Contacts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [email] NVARCHAR(50) NOT NULL UNIQUE, 
    [lastname] NVARCHAR(50) NULL, 
    [firstname] NVARCHAR(50) NULL
)

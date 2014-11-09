CREATE TABLE [dbo].[Contacts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Email] NVARCHAR(250) NOT NULL UNIQUE, 
    [LastName] NVARCHAR(50) NULL, 
    [FirstName] NVARCHAR(50) NULL
)

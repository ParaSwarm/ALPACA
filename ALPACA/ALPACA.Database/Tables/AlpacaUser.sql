CREATE TABLE [dbo].[AlpacaUser]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Email] NVARCHAR(250) NOT NULL UNIQUE,
    [AccountName] NVARCHAR(100) NOT NULL, 
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL, 
    [Contacts] NVARCHAR(MAX) NULL, 
    [EmailPassword] NVARCHAR(50) NULL, 
    [EmailServer] NVARCHAR(50) NULL, 
    [EmailPort] NVARCHAR(50) NULL, 
    [AccountPassword] NVARCHAR(50) NULL
)

CREATE TABLE [dbo].[AlpacaUser]
(
	[alpacauser_key] nvarchar(255) NOT NULL PRIMARY KEY, 
    [Email] NVARCHAR(250) NOT NULL UNIQUE,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL, 
    [Contacts] NVARCHAR(MAX) NULL, 
    [EmailPassword] NVARCHAR(50) NULL, 
    [EmailServer] NVARCHAR(50) NULL, 
    [EmailPort] NVARCHAR(50) NULL, 
    [AdminFlag] BIT NOT NULL DEFAULT(0)
)

CREATE TABLE [dbo].[EmailDraft]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,  
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Body] NVARCHAR(MAX) NOT NULL, 
    --CONSTRAINT [FK_Drafts_AspNetUsers] FOREIGN KEY ([uID]) REFERENCES [AspNetUsers]([Id])
)

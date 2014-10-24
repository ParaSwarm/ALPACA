CREATE TABLE [dbo].[Drafts]
(
	[dID] INT NOT NULL PRIMARY KEY,
	[uID] NVARCHAR(128) NOT NULL, 
    [draftBody] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Drafts_AspNetUsers] FOREIGN KEY ([uID]) REFERENCES [AspNetUsers]([Id])

)

CREATE TABLE [dbo].[EmailDraft] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId] NVARCHAR (255) NOT NULL,
    [Name]   NVARCHAR (255) NOT NULL,
    [Body]   NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



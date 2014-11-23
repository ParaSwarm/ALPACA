CREATE TABLE [dbo].[AspNetUsers] (
    [Id]            NVARCHAR (255) NOT NULL,
    [UserName]      NVARCHAR (255) NULL,
    [PasswordHash]  NVARCHAR (255) NULL,
    [SecurityStamp] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


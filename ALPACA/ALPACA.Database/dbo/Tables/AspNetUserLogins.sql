CREATE TABLE [dbo].[AspNetUserLogins] (
    [UserId]        NVARCHAR (255) NOT NULL,
    [LoginProvider] NVARCHAR (255) NULL,
    [ProviderKey]   NVARCHAR (255) NULL,
    CONSTRAINT [FKEF896DAEEA778823] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


CREATE TABLE [dbo].[PublicationAccess] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [UserID]        INT NOT NULL,
    CONSTRAINT [PK_PublicationAuthor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationAccess_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


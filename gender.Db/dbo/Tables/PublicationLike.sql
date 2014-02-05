CREATE TABLE [dbo].[PublicationLike] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [UserID]        INT NOT NULL,
    [IsLike]        BIT NOT NULL,
    CONSTRAINT [PK_PublicationLike] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationLike_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationLike_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


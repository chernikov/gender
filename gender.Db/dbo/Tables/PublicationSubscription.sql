CREATE TABLE [dbo].[PublicationSubscription] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [UserID]        INT NOT NULL,
    CONSTRAINT [PK_PublicationSubscription] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationSubscription_BlogPost] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationSubscription_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


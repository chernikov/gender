CREATE TABLE [dbo].[PublicationComment] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [CommentID]     INT NOT NULL,
    CONSTRAINT [PK_PublicationComment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationComment_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationComment_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[ImageComment] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [ImageID]   INT NOT NULL,
    [CommentID] INT NOT NULL,
    CONSTRAINT [PK_ImageComment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageComment_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageComment_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[DocumentComment] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [CommentID]  INT NOT NULL,
    CONSTRAINT [PK_DocumentComment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentComment_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentComment_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


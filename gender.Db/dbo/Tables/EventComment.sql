CREATE TABLE [dbo].[EventComment] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [EventID]   INT NOT NULL,
    [CommentID] INT NOT NULL,
    CONSTRAINT [PK_EventComment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventComment_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventComment_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


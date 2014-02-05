CREATE TABLE [dbo].[BlogPostComment] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID] INT NOT NULL,
    [CommentID]  INT NOT NULL,
    CONSTRAINT [PK_BlogPostComment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostComment_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostComment_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


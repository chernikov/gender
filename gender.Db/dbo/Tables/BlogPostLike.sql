CREATE TABLE [dbo].[BlogPostLike] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID] INT NOT NULL,
    [UserID]     INT NOT NULL,
    [IsLike]     BIT NOT NULL,
    CONSTRAINT [PK_BlogPostLike] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostLike_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostLike_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


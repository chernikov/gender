CREATE TABLE [dbo].[BlogPostRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID]       INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_BlogPostRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostRecordRedirect_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


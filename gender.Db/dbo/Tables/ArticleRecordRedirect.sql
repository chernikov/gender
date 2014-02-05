CREATE TABLE [dbo].[ArticleRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [ArticleID]        INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_ArticleRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ArticleRecordRedirect_ArticleRecordRedirect] FOREIGN KEY ([ArticleID]) REFERENCES [dbo].[Article] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ArticleRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


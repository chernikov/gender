CREATE TABLE [dbo].[ArticleSubject] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [ArticleID] INT NOT NULL,
    [SubjectID] INT NOT NULL,
    CONSTRAINT [PK_ArticleSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ArticleSubject_Article] FOREIGN KEY ([ArticleID]) REFERENCES [dbo].[Article] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ArticleSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


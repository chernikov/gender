CREATE TABLE [dbo].[BlogPostSubject] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID] INT NOT NULL,
    [SubjectID]  INT NOT NULL,
    CONSTRAINT [PK_BlogPostSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostSubject_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


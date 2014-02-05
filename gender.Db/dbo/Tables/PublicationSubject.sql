CREATE TABLE [dbo].[PublicationSubject] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [SubjectID]     INT NOT NULL,
    CONSTRAINT [PK_PublicationSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationSubject_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


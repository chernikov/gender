CREATE TABLE [dbo].[PersonSubject] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [PersonID]  INT NOT NULL,
    [SubjectID] INT NOT NULL,
    CONSTRAINT [PK_PersonSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PersonSubject_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


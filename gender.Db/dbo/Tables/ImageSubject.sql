CREATE TABLE [dbo].[ImageSubject] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [ImageID]   INT NOT NULL,
    [SubjectID] INT NOT NULL,
    CONSTRAINT [PK_ImageSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageSubject_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[StudyMaterialSubject] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [SubjectID]       INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialSubject_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


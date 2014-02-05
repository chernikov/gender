CREATE TABLE [dbo].[DocumentSubject] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [SubjectID]  INT NOT NULL,
    CONSTRAINT [PK_DocumentSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentSubject_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[OrganizationSubject] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID] INT NOT NULL,
    [SubjectID]      INT NOT NULL,
    CONSTRAINT [PK_OrganizationSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrganizationSubject_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrganizationSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[EventSubject] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [EventID]   INT NOT NULL,
    [SubjectID] INT NOT NULL,
    CONSTRAINT [PK_EventSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventSubject_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


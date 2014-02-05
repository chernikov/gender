CREATE TABLE [dbo].[SubjectSubscription] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [SubjectID] INT NOT NULL,
    [UserID]    INT NOT NULL,
    CONSTRAINT [PK_SubjectSubscription] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SubjectSubscription_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_SubjectSubscription_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


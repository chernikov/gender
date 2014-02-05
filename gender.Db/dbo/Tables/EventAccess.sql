CREATE TABLE [dbo].[EventAccess] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [EventID] INT NOT NULL,
    [UserID]  INT NOT NULL,
    CONSTRAINT [PK_EventAuthor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventAccess_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


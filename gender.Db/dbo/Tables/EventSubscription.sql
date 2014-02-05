CREATE TABLE [dbo].[EventSubscription] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [EventID] INT NOT NULL,
    [UserID]  INT NOT NULL,
    CONSTRAINT [PK_EventSubscription] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventSubscription_BlogPost] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventSubscription_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


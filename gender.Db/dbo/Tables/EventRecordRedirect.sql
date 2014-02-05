CREATE TABLE [dbo].[EventRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [EventID]          INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_EventRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventRecordRedirect_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


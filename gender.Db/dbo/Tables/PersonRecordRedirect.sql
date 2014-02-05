CREATE TABLE [dbo].[PersonRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [PersonID]         INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_PersonRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PersonRecordRedirect_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


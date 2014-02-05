CREATE TABLE [dbo].[PublicationRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [PublicationID]    INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_PublicationRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationRecordRedirect_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


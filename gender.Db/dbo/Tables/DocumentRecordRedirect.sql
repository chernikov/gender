CREATE TABLE [dbo].[DocumentRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [DocumentID]       INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_DocumentRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentRecordRedirect_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


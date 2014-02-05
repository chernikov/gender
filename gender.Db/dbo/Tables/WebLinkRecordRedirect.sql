CREATE TABLE [dbo].[WebLinkRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [WebLinkID]        INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_WebLinkRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WebLinkRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_WebLinkRecordRedirect_WebLink] FOREIGN KEY ([WebLinkID]) REFERENCES [dbo].[WebLink] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


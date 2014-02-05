CREATE TABLE [dbo].[ImageRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [ImageID]          INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_ImageRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageRecordRedirect_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


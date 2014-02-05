CREATE TABLE [dbo].[OrganizationRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID]   INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_OrganizationRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrganizationRecordRedirect_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrganizationRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


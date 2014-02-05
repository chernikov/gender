CREATE TABLE [dbo].[Mail] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [DistributionID] INT            NULL,
    [UserID]         INT            NOT NULL,
    [Email]          NVARCHAR (500) NOT NULL,
    [Subject]        NVARCHAR (500) NOT NULL,
    [Body]           NVARCHAR (MAX) NOT NULL,
    [AddedDate]      DATETIME       NOT NULL,
    [ProcessedDate]  DATETIME       NULL,
    CONSTRAINT [PK_Mail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Mail_Distribution] FOREIGN KEY ([DistributionID]) REFERENCES [dbo].[Distribution] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Mail_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


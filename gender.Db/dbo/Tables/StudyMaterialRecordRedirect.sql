CREATE TABLE [dbo].[StudyMaterialRecordRedirect] (
    [ID]               INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID]  INT NOT NULL,
    [RecordRedirectID] INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialRecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialRecordRedirect_RecordRedirect] FOREIGN KEY ([RecordRedirectID]) REFERENCES [dbo].[RecordRedirect] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialRecordRedirect_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


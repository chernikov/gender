CREATE TABLE [dbo].[DocumentRegion] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [RegionID]   INT NOT NULL,
    CONSTRAINT [PK_DocumentRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentRegion_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[PublicationRegion] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [RegionID]      INT NOT NULL,
    CONSTRAINT [PK_PublicationRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationRegion_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


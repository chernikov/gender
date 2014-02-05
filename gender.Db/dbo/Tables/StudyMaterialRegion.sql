CREATE TABLE [dbo].[StudyMaterialRegion] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [RegionID]        INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialRegion_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


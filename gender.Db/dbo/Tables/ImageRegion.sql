﻿CREATE TABLE [dbo].[ImageRegion] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [ImageID]  INT NOT NULL,
    [RegionID] INT NOT NULL,
    CONSTRAINT [PK_ImageRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageRegion_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


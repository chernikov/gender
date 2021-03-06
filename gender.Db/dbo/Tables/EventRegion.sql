﻿CREATE TABLE [dbo].[EventRegion] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [EventID]  INT NOT NULL,
    [RegionID] INT NOT NULL,
    CONSTRAINT [PK_EventRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventRegion_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


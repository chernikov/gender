CREATE TABLE [dbo].[OrganizationRegion] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID] INT NOT NULL,
    [RegionID]       INT NOT NULL,
    CONSTRAINT [PK_OrganizationRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrganizationRegion_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrganizationRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


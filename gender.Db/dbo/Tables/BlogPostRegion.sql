CREATE TABLE [dbo].[BlogPostRegion] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID] INT NOT NULL,
    [RegionID]   INT NOT NULL,
    CONSTRAINT [PK_BlogPostRegion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostRegion_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostRegion_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


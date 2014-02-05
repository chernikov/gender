CREATE TABLE [dbo].[StudyMaterialLink] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [LinkID]          INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialLink] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialLink_Link] FOREIGN KEY ([LinkID]) REFERENCES [dbo].[Link] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialLink_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


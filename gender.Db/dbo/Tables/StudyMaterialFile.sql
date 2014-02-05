CREATE TABLE [dbo].[StudyMaterialFile] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [FileID]          INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialFile] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialFile_File] FOREIGN KEY ([FileID]) REFERENCES [dbo].[File] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialFile_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


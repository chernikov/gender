CREATE TABLE [dbo].[StudyMaterialAccess] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [UserID]          INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialAuthor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialAccess_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


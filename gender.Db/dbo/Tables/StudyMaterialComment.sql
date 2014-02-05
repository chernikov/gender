CREATE TABLE [dbo].[StudyMaterialComment] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [CommentID]       INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialComment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialComment_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialComment_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[StudyMaterialPerson] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [StudyMaterialID] INT NOT NULL,
    [PersonID]        INT NOT NULL,
    CONSTRAINT [PK_StudyMaterialPerson] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterialPerson_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_StudyMaterialPerson_StudyMaterial] FOREIGN KEY ([StudyMaterialID]) REFERENCES [dbo].[StudyMaterial] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


﻿CREATE TABLE [dbo].[ImagePerson] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [ImageID]  INT NOT NULL,
    [PersonID] INT NOT NULL,
    CONSTRAINT [PK_ImagePerson] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImagePerson_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImagePerson_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

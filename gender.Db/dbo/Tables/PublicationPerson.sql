CREATE TABLE [dbo].[PublicationPerson] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [PersonID]      INT NOT NULL,
    CONSTRAINT [PK_PublicationPerson] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationPerson_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationPerson_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


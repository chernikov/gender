CREATE TABLE [dbo].[PublicationFile] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [FileID]        INT NOT NULL,
    CONSTRAINT [PK_PublicationFile] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationFile_File] FOREIGN KEY ([FileID]) REFERENCES [dbo].[File] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationFile_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[DocumentFile] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [FileID]     INT NOT NULL,
    CONSTRAINT [PK_DocumentFile] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentFile_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentFile_File] FOREIGN KEY ([FileID]) REFERENCES [dbo].[File] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[DocumentLink] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [LinkID]     INT NOT NULL,
    CONSTRAINT [PK_DocumentLink] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentLink_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentLink_Link] FOREIGN KEY ([LinkID]) REFERENCES [dbo].[Link] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


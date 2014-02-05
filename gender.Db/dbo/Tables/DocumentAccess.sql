CREATE TABLE [dbo].[DocumentAccess] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [UserID]     INT NOT NULL,
    CONSTRAINT [PK_DocumentAuthor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentAccess_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


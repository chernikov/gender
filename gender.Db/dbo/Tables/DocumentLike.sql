CREATE TABLE [dbo].[DocumentLike] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [UserID]     INT NOT NULL,
    [IsLike]     BIT NOT NULL,
    CONSTRAINT [PK_DocumentLike] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentLike_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentLike_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[ImageAccess] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [ImageID] INT NOT NULL,
    [UserID]  INT NOT NULL,
    CONSTRAINT [PK_ImageAuthor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageAccess_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


CREATE TABLE [dbo].[PublicationLink] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [PublicationID] INT NOT NULL,
    [LinkID]        INT NOT NULL,
    [IsShop]        BIT NOT NULL,
    CONSTRAINT [PK_PublicationLink] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationLink_Link] FOREIGN KEY ([LinkID]) REFERENCES [dbo].[Link] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationLink_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


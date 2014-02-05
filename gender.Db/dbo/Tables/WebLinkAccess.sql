CREATE TABLE [dbo].[WebLinkAccess] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [WebLinkID] INT NOT NULL,
    [UserID]    INT NOT NULL,
    CONSTRAINT [PK_WebLinkAuthor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WebLinkAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_WebLinkAccess_WebLink] FOREIGN KEY ([WebLinkID]) REFERENCES [dbo].[WebLink] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


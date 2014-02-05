CREATE TABLE [dbo].[WebLinkSubscription] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [WebLinkID] INT NOT NULL,
    [UserID]    INT NOT NULL,
    CONSTRAINT [PK_WebLinkSubscription] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WebLinkSubscription_BlogPost] FOREIGN KEY ([WebLinkID]) REFERENCES [dbo].[WebLink] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_WebLinkSubscription_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


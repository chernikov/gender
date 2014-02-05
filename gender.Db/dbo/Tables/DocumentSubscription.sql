CREATE TABLE [dbo].[DocumentSubscription] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [DocumentID] INT NOT NULL,
    [UserID]     INT NOT NULL,
    CONSTRAINT [PK_DocumentSubscription] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentSubscription_BlogPost] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentSubscription_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


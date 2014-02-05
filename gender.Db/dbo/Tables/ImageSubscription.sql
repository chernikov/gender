CREATE TABLE [dbo].[ImageSubscription] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [ImageID] INT NOT NULL,
    [UserID]  INT NOT NULL,
    CONSTRAINT [PK_ImageSubscription] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageSubscription_BlogPost] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageSubscription_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


CREATE TABLE [dbo].[SubscriptionPart] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [UserID]      INT            NOT NULL,
    [AddedDate]   DATETIME       NOT NULL,
    [Text]        NVARCHAR (MAX) NOT NULL,
    [UpdateType]  INT            NOT NULL,
    [IsProcessed] BIT            NOT NULL,
    CONSTRAINT [PK_SubscriptionPart] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SubscriptionPart_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


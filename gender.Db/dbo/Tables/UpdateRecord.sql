CREATE TABLE [dbo].[UpdateRecord] (
    [ID]           INT      IDENTITY (1, 1) NOT NULL,
    [AddedDate]    DATETIME NOT NULL,
    [UserID]       INT      NULL,
    [Type]         INT      NOT NULL,
    [MaterialType] INT      NOT NULL,
    [ResourceID]   INT      NOT NULL,
    CONSTRAINT [PK_UpdateRecord] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UpdateRecord_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


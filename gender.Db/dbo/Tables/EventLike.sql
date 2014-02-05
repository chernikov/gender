﻿CREATE TABLE [dbo].[EventLike] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [EventID] INT NOT NULL,
    [UserID]  INT NOT NULL,
    [IsLike]  BIT NOT NULL,
    CONSTRAINT [PK_EventLike] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventLike_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventLike_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

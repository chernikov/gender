﻿CREATE TABLE [dbo].[EventLink] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [EventID] INT NOT NULL,
    [LinkID]  INT NOT NULL,
    CONSTRAINT [PK_EventLink] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventLink_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventLink_Link] FOREIGN KEY ([LinkID]) REFERENCES [dbo].[Link] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


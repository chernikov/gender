﻿CREATE TABLE [dbo].[EventFile] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [EventID] INT NOT NULL,
    [FileID]  INT NOT NULL,
    CONSTRAINT [PK_EventFile] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventFile_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventFile_File] FOREIGN KEY ([FileID]) REFERENCES [dbo].[File] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


﻿CREATE TABLE [dbo].[EventPerson] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [EventID]  INT NOT NULL,
    [PersonID] INT NOT NULL,
    CONSTRAINT [PK_EventPerson] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventPerson_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventPerson_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


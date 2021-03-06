﻿CREATE TABLE [dbo].[WebLinkSubject] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [WebLinkID] INT NOT NULL,
    [SubjectID] INT NOT NULL,
    CONSTRAINT [PK_WebLinkSubject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WebLinkSubject_Subject] FOREIGN KEY ([SubjectID]) REFERENCES [dbo].[Subject] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_WebLinkSubject_WebLink] FOREIGN KEY ([WebLinkID]) REFERENCES [dbo].[WebLink] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


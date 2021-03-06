﻿CREATE TABLE [dbo].[Article] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [Header] NVARCHAR (500) NOT NULL,
    [Url]    NVARCHAR (500) NOT NULL,
    [Text]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[BlogParser] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [BlogID]     INT            NOT NULL,
    [Link]       NVARCHAR (500) NOT NULL,
    [Type]       INT            NOT NULL,
    [LastUpdate] DATETIME       NULL,
    CONSTRAINT [PK_BlogParser] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogParser_Blog] FOREIGN KEY ([BlogID]) REFERENCES [dbo].[Blog] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


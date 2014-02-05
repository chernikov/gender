CREATE TABLE [dbo].[BlogPost] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [BlogID]      INT            NOT NULL,
    [LinkID]      INT            NULL,
    [Header]      NVARCHAR (500) NOT NULL,
    [Url]         NVARCHAR (500) NOT NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [AddedDate]   DATETIME       NOT NULL,
    [ChangedDate] DATETIME       NOT NULL,
    [Source]      NVARCHAR (500) NULL,
    [TotalLikes]  INT            NOT NULL,
    CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPost_Blog] FOREIGN KEY ([BlogID]) REFERENCES [dbo].[Blog] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPost_Link] FOREIGN KEY ([LinkID]) REFERENCES [dbo].[Link] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE [dbo].[Publication] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [UserID]        INT             NOT NULL,
    [ParentID]      INT             NULL,
    [Cover]         NVARCHAR (150)  NULL,
    [Header]        NVARCHAR (2000) NOT NULL,
    [Url]           NVARCHAR (2000) NOT NULL,
    [Bibliographic] NVARCHAR (MAX)  NOT NULL,
    [Year]          INT             NULL,
    [Type]          INT             NOT NULL,
    [Teaser]        NVARCHAR (MAX)  NOT NULL,
    [Content]       NVARCHAR (MAX)  NOT NULL,
    [AddedDate]     DATETIME        NOT NULL,
    [ChangedDate]   DATETIME        NOT NULL,
    [ModeratedDate] DATETIME        NULL,
    [TotalLikes]    INT             NOT NULL,
    CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Publication_Parent] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Publication] ([ID]),
    CONSTRAINT [FK_Publication_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


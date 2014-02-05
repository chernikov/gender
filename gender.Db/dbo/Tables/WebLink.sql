CREATE TABLE [dbo].[WebLink] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (500)  NOT NULL,
    [SiteUrl]       NVARCHAR (500)  NOT NULL,
    [Screenshot]    NVARCHAR (150)  NOT NULL,
    [Url]           NVARCHAR (1024) NOT NULL,
    [ReservedUrl]   NVARCHAR (1024) NULL,
    [Description]   NVARCHAR (MAX)  NULL,
    [RSS]           NVARCHAR (1024) NULL,
    [AddedDate]     DATETIME        NOT NULL,
    [ChangedDate]   DATETIME        NOT NULL,
    [ModeratedDate] DATETIME        NULL,
    [TotalLikes]    INT             NOT NULL,
    CONSTRAINT [PK_WebLink] PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[Image] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Path]          NVARCHAR (150) NOT NULL,
    [Header]        NVARCHAR (500) NOT NULL,
    [Url]           NVARCHAR (500) NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ChangedDate]   DATETIME       NOT NULL,
    [ModeratedDate] DATETIME       NULL,
    [TotalLikes]    INT            NOT NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([ID] ASC)
);


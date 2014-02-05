CREATE TABLE [dbo].[User] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [Login]               NVARCHAR (150) NOT NULL,
    [Password]            NVARCHAR (50)  NULL,
    [AddedDate]           DATETIME       NULL,
    [ActivatedDate]       DATETIME       NULL,
    [ActivatedLink]       NVARCHAR (50)  NOT NULL,
    [LastVisitDate]       DATETIME       NOT NULL,
    [Invited]             BIT            NOT NULL,
    [Rating]              INT            NOT NULL,
    [StartRating]         INT            NOT NULL,
    [NoticeCommentPeriod] INT            NOT NULL,
    [NoticeUpdatePeriod]  INT            NOT NULL,
    [Category]            INT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC)
);


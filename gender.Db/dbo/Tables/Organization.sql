CREATE TABLE [dbo].[Organization] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [UserID]      INT            NOT NULL,
    [Name]        NVARCHAR (500) NOT NULL,
    [Url]         NVARCHAR (500) NOT NULL,
    [Logo]        NVARCHAR (150) NULL,
    [Info]        NVARCHAR (MAX) NOT NULL,
    [AddedDate]   DATETIME       NOT NULL,
    [ChangedDate] DATETIME       NOT NULL,
	[ModeratedDate] DATETIME     NULL,
    [TotalLikes]  INT            NOT NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Organization_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


CREATE TABLE [dbo].[Event] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Image]         NVARCHAR (150) NULL,
    [Header]        NVARCHAR (500) NOT NULL,
    [Url]           NVARCHAR (500) NOT NULL,
    [Teaser]        NVARCHAR (MAX) NULL,
    [EventDate]     DATETIME       NULL,
    [Year]          INT            NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ChangedDate]   DATETIME       NOT NULL,
    [ModeratedDate] DATETIME       NULL,
    [TotalLikes]    INT            NOT NULL,
    [UserID]        INT            NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Event_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


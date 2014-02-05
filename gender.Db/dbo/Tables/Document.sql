CREATE TABLE [dbo].[Document] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NOT NULL,
    [EventID]       INT            NULL,
    [Header]        NVARCHAR (500) NOT NULL,
    [Url]           NVARCHAR (500) NOT NULL,
    [Teaser]        NVARCHAR (MAX) NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ChangedDate]   DATETIME       NOT NULL,
    [ModeratedDate] DATETIME       NULL,
    [TotalLikes]    INT            NOT NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Document_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE SET NULL ON UPDATE SET NULL,
    CONSTRAINT [FK_Document_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


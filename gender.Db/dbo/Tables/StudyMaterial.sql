CREATE TABLE [dbo].[StudyMaterial] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NOT NULL,
    [Name]          NVARCHAR (500) NOT NULL,
    [Url]           NVARCHAR (500) NOT NULL,
    [Teaser]        NVARCHAR (MAX) NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ChangedDate]   DATETIME       NOT NULL,
    [ModeratedDate] DATETIME       NULL,
    [TotalLikes]    INT            NOT NULL,
    CONSTRAINT [PK_StudyMaterial] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudyMaterial_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


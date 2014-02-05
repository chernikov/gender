CREATE TABLE [dbo].[Person] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NULL,
    [AuthorID]      INT            NOT NULL,
    [FirstName]     NVARCHAR (150) NOT NULL,
    [LastName]      NVARCHAR (150) NOT NULL,
    [Patronymic]    NVARCHAR (150) NULL,
    [Url]           NVARCHAR (500) NOT NULL,
    [Photo]         NVARCHAR (150) NULL,
    [Bio]           NVARCHAR (MAX) NULL,
    [Category]      INT            NOT NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ChangedDate]   DATETIME       NOT NULL,
    [ModeratedDate] DATETIME       NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Person_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Person_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


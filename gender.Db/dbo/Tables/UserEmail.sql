CREATE TABLE [dbo].[UserEmail] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [UserID]       INT            NOT NULL,
    [Email]        NVARCHAR (150) NOT NULL,
    [ActivateLink] NVARCHAR (500) NOT NULL,
    [ActivateDate] DATETIME       NULL,
    [IsPrimary]    BIT            NOT NULL,
    [Sended]       BIT            NOT NULL,
    CONSTRAINT [PK_UserEmail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserEmail_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


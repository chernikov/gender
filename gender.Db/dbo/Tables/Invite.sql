CREATE TABLE [dbo].[Invite] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [UserID]    INT            NOT NULL,
    [InvitedID] INT            NULL,
    [Email]     NVARCHAR (500) NOT NULL,
    [Link]      NVARCHAR (500) NOT NULL,
    [AddedDate] DATETIME       NOT NULL,
    [UsedDate]  DATETIME       NULL,
    CONSTRAINT [PK_Invite] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Invite_Invited] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Invite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


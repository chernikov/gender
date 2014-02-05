CREATE TABLE [dbo].[UserSocial] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [UserID]       INT            NOT NULL,
    [Provider]     INT            NOT NULL,
    [Link]         NVARCHAR (500) NOT NULL,
    [Identified]   NVARCHAR (MAX) NULL,
    [UserInfo]     NVARCHAR (MAX) NULL,
    [JsonResource] NVARCHAR (MAX) NULL,
    [ExpiredBy]    DATETIME       NULL,
    CONSTRAINT [PK_UserSocial] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserSocial_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


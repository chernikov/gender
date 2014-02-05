CREATE TABLE [dbo].[OrganizationAccess] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID] INT NOT NULL,
    [UserID]         INT NOT NULL,
    CONSTRAINT [PK_OrganizationAccess] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrganizationAccess_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrganizationAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


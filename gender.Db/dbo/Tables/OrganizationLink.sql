CREATE TABLE [dbo].[OrganizationLink] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID] INT NOT NULL,
    [LinkID]         INT NOT NULL,
    CONSTRAINT [PK_OrganizationLink] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrganizationLink_Link] FOREIGN KEY ([LinkID]) REFERENCES [dbo].[Link] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrganizationLink_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


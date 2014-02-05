CREATE TABLE [dbo].[OrganizationContact] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID] INT NOT NULL,
    [ContactID]      INT NOT NULL,
    CONSTRAINT [PK_OrganizationContact] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrganizationContact_Contact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[Contact] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_OrganizationContact_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


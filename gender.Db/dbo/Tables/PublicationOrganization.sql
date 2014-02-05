CREATE TABLE [dbo].[PublicationOrganization] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [OrganizationID] INT NOT NULL,
    [PublicationID]  INT NOT NULL,
    CONSTRAINT [PK_OrganizationPublication] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PublicationOrganization_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PublicationOrganization_Publication] FOREIGN KEY ([PublicationID]) REFERENCES [dbo].[Publication] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


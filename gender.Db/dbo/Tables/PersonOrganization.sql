CREATE TABLE [dbo].[PersonOrganization] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [PersonID]       INT NOT NULL,
    [OrganizationID] INT NOT NULL,
    CONSTRAINT [PK_PersonOrganization] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PersonOrganization_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonOrganization_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


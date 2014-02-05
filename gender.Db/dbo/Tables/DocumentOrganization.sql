CREATE TABLE [dbo].[DocumentOrganization] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [DocumentID]     INT NOT NULL,
    [OrganizationID] INT NOT NULL,
    CONSTRAINT [PK_DocumentOrganization] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DocumentOrganization_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DocumentOrganization_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


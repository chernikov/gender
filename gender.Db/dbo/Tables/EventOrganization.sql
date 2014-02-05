CREATE TABLE [dbo].[EventOrganization] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [EventID]        INT NOT NULL,
    [OrganizationID] INT NOT NULL,
    CONSTRAINT [PK_EventOrganization] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventOrganization_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EventOrganization_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


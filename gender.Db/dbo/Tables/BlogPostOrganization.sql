CREATE TABLE [dbo].[BlogPostOrganization] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID]     INT NOT NULL,
    [OrganizationID] INT NOT NULL,
    CONSTRAINT [PK_BlogPostOrganization] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostOrganization_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostOrganization_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


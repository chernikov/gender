CREATE TABLE [dbo].[BlogPostPerson] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [BlogPostID] INT NOT NULL,
    [PersonID]   INT NOT NULL,
    CONSTRAINT [PK_BlogPostPerson] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BlogPostPerson_BlogPost] FOREIGN KEY ([BlogPostID]) REFERENCES [dbo].[BlogPost] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BlogPostPerson_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


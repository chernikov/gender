CREATE TABLE [dbo].[Subject] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [ParentID] INT            NULL,
    [Name]     NVARCHAR (500) NOT NULL,
    [Url]      NVARCHAR (500) NOT NULL,
    [OrderBy]  INT            NOT NULL,
    [MainShow] BIT            NOT NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Subject_Parent] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Subject] ([ID])
);


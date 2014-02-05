CREATE TABLE [dbo].[Region] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [ParentID]    INT            NULL,
    [Name]        NVARCHAR (500) NOT NULL,
    [Url]         NVARCHAR (500) NOT NULL,
    [OrderBy]     INT            NOT NULL,
    [Map]         NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Link]        NVARCHAR (500) NULL,
    [HasEntry]    BIT            NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Region_Parent] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Region] ([ID])
);


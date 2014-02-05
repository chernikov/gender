CREATE TABLE [dbo].[Setting] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (500) NOT NULL,
    [Value]       NVARCHAR (MAX) NULL,
    [ValueInt]    INT            NULL,
    [ValueDouble] FLOAT (53)     NULL,
    CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED ([ID] ASC)
);


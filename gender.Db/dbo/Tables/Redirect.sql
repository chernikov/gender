CREATE TABLE [dbo].[Redirect] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [OldLink] NVARCHAR (500) NOT NULL,
    [NewLink] NVARCHAR (500) NOT NULL,
    [IsForum] BIT            NOT NULL,
    CONSTRAINT [PK_Redirect] PRIMARY KEY CLUSTERED ([ID] ASC)
);


CREATE TABLE [dbo].[Distribution] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [AddedDate] DATETIME       NOT NULL,
    [Subject]   NVARCHAR (MAX) NOT NULL,
    [Body]      NVARCHAR (MAX) NOT NULL,
    [IsStart]   BIT            NOT NULL,
    CONSTRAINT [PK_Distribution] PRIMARY KEY CLUSTERED ([ID] ASC)
);


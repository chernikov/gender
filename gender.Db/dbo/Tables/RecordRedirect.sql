CREATE TABLE [dbo].[RecordRedirect] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [Url]     NVARCHAR (MAX) NOT NULL,
    [IsForum] BIT            NOT NULL,
    CONSTRAINT [PK_RecordRedirect] PRIMARY KEY CLUSTERED ([ID] ASC)
);


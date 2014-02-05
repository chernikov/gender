CREATE TABLE [dbo].[Contact] (
    [ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Type]  INT            NOT NULL,
    [Value] NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ID] ASC)
);


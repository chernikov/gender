CREATE TABLE [dbo].[SubscriptionTemplate] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [Type]            INT            NOT NULL,
    [TemplateSample]  NVARCHAR (500) NOT NULL,
    [CountParameters] INT            NOT NULL,
    [IsActive]        BIT            NOT NULL,
    [Template]        NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_SubscriptionTemplate] PRIMARY KEY CLUSTERED ([ID] ASC)
);


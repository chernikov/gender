CREATE TABLE [dbo].[File] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Path]      NVARCHAR (150) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [AddedDate] DATETIME       NOT NULL,
    [IsImage]   BIT            NOT NULL,
    [MimeType]  NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([ID] ASC)
);


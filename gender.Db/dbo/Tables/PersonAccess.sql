CREATE TABLE [dbo].[PersonAccess] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [PersonID] INT NOT NULL,
    [UserID]   INT NOT NULL,
    CONSTRAINT [PK_PesonAccess] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PersonAccess_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonAccess_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


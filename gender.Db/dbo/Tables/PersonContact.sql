CREATE TABLE [dbo].[PersonContact] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [ContactID] INT NOT NULL,
    [PersonID]  INT NOT NULL,
    CONSTRAINT [PK_PersonContact] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PersonContact_Contact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[Contact] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PersonContact_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


PRINT N'Creating [dbo].[UserRole]...';


GO
CREATE TABLE [dbo].[UserRole] (
    [ID]     INT IDENTITY (1, 1) NOT NULL,
    [RoleID] INT NOT NULL,
    [UserID] INT NOT NULL
);


GO
PRINT N'Creating PK_UserRole...';


GO
ALTER TABLE [dbo].[UserRole]
    ADD CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Language]...';


GO
CREATE TABLE [dbo].[Language] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (50) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL
);


GO
PRINT N'Creating PK_Language...';


GO
ALTER TABLE [dbo].[Language]
    ADD CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Email]         NVARCHAR (150) NOT NULL,
    [Password]      NVARCHAR (50)  NOT NULL,
    [LanguageID]    INT            NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ActivatedDate] DATETIME       NULL,
    [ActivatedLink] NVARCHAR (50)  NOT NULL,
    [LastVisitDate] DATETIME       NOT NULL,
    [AvatarPath]    NVARCHAR (150) NULL
);


GO
PRINT N'Creating PK_User...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[UserLang]...';


GO
CREATE TABLE [dbo].[UserLang] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [UserID]     INT            NOT NULL,
    [LanguageID] INT            NOT NULL,
    [FirstName]  NVARCHAR (500) NULL,
    [LastName]   NVARCHAR (500) NULL
);


GO
PRINT N'Creating PK_UserLang...';


GO
ALTER TABLE [dbo].[UserLang]
    ADD CONSTRAINT [PK_UserLang] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Role]...';


GO
CREATE TABLE [dbo].[Role] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (50) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL
);


GO
PRINT N'Creating PK_Role...';


GO
ALTER TABLE [dbo].[Role]
    ADD CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating FK_UserRole_Role...';


GO
ALTER TABLE [dbo].[UserRole] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating FK_UserRole_User...';


GO
ALTER TABLE [dbo].[UserRole] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating FK_User_Language...';


GO
ALTER TABLE [dbo].[User] WITH NOCHECK
    ADD CONSTRAINT [FK_User_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_UserLang_Language...';


GO
ALTER TABLE [dbo].[UserLang] WITH NOCHECK
    ADD CONSTRAINT [FK_UserLang_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating FK_UserLang_User...';


GO
ALTER TABLE [dbo].[UserLang] WITH NOCHECK
    ADD CONSTRAINT [FK_UserLang_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_Role];

ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_User];

ALTER TABLE [dbo].[User] WITH CHECK CHECK CONSTRAINT [FK_User_Language];

ALTER TABLE [dbo].[UserLang] WITH CHECK CHECK CONSTRAINT [FK_UserLang_Language];

ALTER TABLE [dbo].[UserLang] WITH CHECK CHECK CONSTRAINT [FK_UserLang_User];


GO

BEGIN TRANSACTION
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role]
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User]
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Language]
ALTER TABLE [dbo].[UserLang] DROP CONSTRAINT [FK_UserLang_Language]
ALTER TABLE [dbo].[UserLang] DROP CONSTRAINT [FK_UserLang_User]
SET IDENTITY_INSERT [dbo].[Language] ON
INSERT INTO [dbo].[Language] ([ID], [Code], [Name]) VALUES (1, N'ru', N'Русский')
INSERT INTO [dbo].[Language] ([ID], [Code], [Name]) VALUES (2, N'en', N'Английский')
SET IDENTITY_INSERT [dbo].[Language] OFF
SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([ID], [Email], [Password], [LanguageID], [AddedDate], [ActivatedDate], [ActivatedLink], [LastVisitDate], [AvatarPath]) VALUES (1, N'admin', N'admin', 2, '20010101 00:00:00.000', '20010101 00:00:00.000', N'', '20120928 14:49:10.843', NULL)
INSERT INTO [dbo].[User] ([ID], [Email], [Password], [LanguageID], [AddedDate], [ActivatedDate], [ActivatedLink], [LastVisitDate], [AvatarPath]) VALUES (9, N'chernikov@gmail.com', N'nbgjuhfabz', NULL, '20121120 18:51:00.853', NULL, N'eed74d9c54fb43078f839793a707dac4', '20121120 18:51:00.853', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT INTO [dbo].[Role] ([ID], [Code], [Name]) VALUES (1, N'admin', N'Админ')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[UserRole] ON
INSERT INTO [dbo].[UserRole] ([ID], [RoleID], [UserID]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID])
ALTER TABLE [dbo].[UserLang] ADD CONSTRAINT [FK_UserLang_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
ALTER TABLE [dbo].[UserLang] ADD CONSTRAINT [FK_UserLang_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
COMMIT TRANSACTION
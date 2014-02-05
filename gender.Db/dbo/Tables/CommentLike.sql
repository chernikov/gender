﻿CREATE TABLE [dbo].[CommentLike] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [CommentID] INT NOT NULL,
    [UserID]    INT NOT NULL,
    [IsLike]    BIT NOT NULL,
    CONSTRAINT [PK_CommentLike] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommentLike_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_CommentLike_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


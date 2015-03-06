﻿CREATE TABLE [dbo].[Users]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(40) NOT NULL, 
    [PasswordSalt] UNIQUEIDENTIFIER NOT NULL, 
    [Email] VARCHAR(50) NOT NULL,
    [CreatedDtUtc] DATETIME2 NOT NULL, 
    [Status] BIT NOT NULL, 
)
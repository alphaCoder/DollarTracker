CREATE TABLE [dbo].[User]
(
	[UserId] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NOT NULL,
    [CreatedDtUtc] DATETIME2 NOT NULL, 
    [Status] BIT NOT NULL, 
    [DisplayName] VARCHAR(50) NULL, 
    [ProfilePic] VARCHAR(MAX) NULL, 
)

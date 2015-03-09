CREATE TABLE [dbo].[Collaborator]
(
	[CollaboratorId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [UserStoryId] VARCHAR(20) NOT NULL, 
    [CreatedUtcDt] DATETIME2 NOT NULL, 
    [Status] BIT NOT NULL
)

CREATE TABLE [dbo].[Collaborator]
(
	[CollaboratorId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [ExpenseStoryId] VARCHAR(20) NOT NULL, 
    [CreatedUtcDt] DATETIME2 NOT NULL, 
    [Status] BIT NOT NULL, 
    CONSTRAINT [FK_Collaborator_ToExpenseStory] FOREIGN KEY ([ExpenseStoryId]) REFERENCES [ExpenseStory]([ExpenseStoryId])
)

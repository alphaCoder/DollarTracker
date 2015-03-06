CREATE TABLE [dbo].[ExpenseStories]
(
	[ExpenseStoryId] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [ExpenseStoryTypeId] UNIQUEIDENTIFIER NOT NULL,
	[Budget] FLOAT(7) NULL, 
    [Income] FLOAT(7) NULL, 
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [StartDt] DATETIME2 NOT NULL, 
    [EndDt] DATETIME2 NOT NULL, 
    [CreatedUtcDt] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_ExpenseStories_ToExpenseStoryTypes] FOREIGN KEY ([ExpenseStoryTypeId]) REFERENCES [ExpenseStoryTypes]([ExpenseStoryTypeId])
)

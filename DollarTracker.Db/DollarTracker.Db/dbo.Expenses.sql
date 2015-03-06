CREATE TABLE [dbo].[Expenses]
(
	[ExpenseId] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [ExpenseStoryId] VARCHAR(20) NOT NULL, 
    [CollaboratorId] UNIQUEIDENTIFIER NOT NULL,
    [Amount] FLOAT NOT NULL DEFAULT 0.0,
	[Receipt] IMAGE NULL, 
    [CreatedUtcDt] DATETIME2 NULL, 
    [Comments] VARCHAR(140) NULL, 
    [ExpenseCategoryId] VARCHAR(20) NULL, 
    [CustomExpenseCategoryId] VARCHAR(20) NULL, 
    CONSTRAINT [FK_Expenses_ToExpenseStories] FOREIGN KEY ([ExpenseStoryId]) REFERENCES [ExpenseStories]([ExpenseStoryId]), 
    CONSTRAINT [FK_Expenses_ToCollaborators] FOREIGN KEY ([CollaboratorId]) REFERENCES [Collaborators]([CollaboratorId]), 
    CONSTRAINT [FK_Expenses_ToExpenseCategories] FOREIGN KEY ([ExpenseCategoryId]) REFERENCES [ExpenseCategories]([ExpenseCategoryId]), 
    CONSTRAINT [FK_Expenses_ToCustomExpenseCategories] FOREIGN KEY ([CustomExpenseCategoryId]) REFERENCES [CustomExpenseCategories]([CustomExpenseCategoryId])
)

GO

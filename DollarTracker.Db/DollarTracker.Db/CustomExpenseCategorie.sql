CREATE TABLE [dbo].[CustomExpenseCategory]
(
	[CustomExpenseCategoryId] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Description] VARCHAR(50) NOT NULL, 
    [CreatedDt] DATETIME2 NULL, 
    [Status] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_CustomExpenseCategories_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]) 
)

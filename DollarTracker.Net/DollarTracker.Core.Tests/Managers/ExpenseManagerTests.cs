using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DollarTracker.EF;
using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Utils;

namespace DollarTracker.Core.Tests.Managers
{
	/// <summary>
	/// Summary description for ExpenseManagerTests
	/// </summary>
	[TestClass]
	public class ExpenseManagerTests : TestEnvironmentBase
	{
		private readonly User user;
		private readonly ExpenseStory expenseStory;
		private readonly ExpenseManager expenseManager;
		private readonly ExpenseRepository expenseRepository;
		private readonly Collaborator collaborator;
		public ExpenseManagerTests()
		{
			user = GetNewMockUser();
			new UserManager(new UserRepository(dbFactory), unitOfWork).AddUser(user);
			expenseStory = GetNewMockPersonalExpenseStory(user.UserId);
			new ExpenseStoryManager(new ExpenseStoryRepository(dbFactory), unitOfWork).AddExpenseStory(expenseStory);

			collaborator = GetNewMockCollaborator(user.UserId, expenseStory.ExpenseStoryId);
			//todo: replace with collaborator manager
			dataContext.Collaborator.Add(collaborator);
			dataContext.SaveChanges();
		}

		[TestMethod]
		public void AddExpenseTest()
		{
			//
			// TODO: Add test logic here
			//
		}


		private Expense GetNewMockExpense(Guid collaboratorId, string expenseStoryId)
		{
			return new Expense
			{
				ExpenseId = UniqueKeyGenerator.DatePrefixShortKey(),
				CreatedUtcDt = DateTime.UtcNow,
				ExpenseStoryId = expenseStoryId,
				CollaboratorId = collaboratorId,
				ExpenseCategoryId = "Groceries"
			};
		}

		private Collaborator GetNewMockCollaborator(Guid userId, string storyId)
		{
			return new Collaborator
			{
				CollaboratorId = Guid.NewGuid(),
				UserStoryId = storyId,
				UserId = userId,
				CreatedUtcDt = DateTime.UtcNow,
				Status = true
			};
		}
	}
}

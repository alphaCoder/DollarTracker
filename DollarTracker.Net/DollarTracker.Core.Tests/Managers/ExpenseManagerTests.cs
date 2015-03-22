using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DollarTracker.EF;
using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Utils;
using System.Linq;

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
			dataContext.Collaborator.Add(collaborator);
			dataContext.SaveChanges();
			expenseRepository = new ExpenseRepository(dbFactory);
			expenseManager = new ExpenseManager(expenseRepository, unitOfWork);
		}

		[TestMethod]
		public void AddExpenseTest()
		{
			var expectedExpense = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);

			expenseManager.AddExpense(expectedExpense);
			var acutalExpense = dataContext.Expense.FirstOrDefault(x => x.ExpenseId == expectedExpense.ExpenseId);

			Assert.IsNotNull(acutalExpense);
			Assert.AreEqual(expectedExpense.GetHashCode(), acutalExpense.GetHashCode());
		}

		[TestMethod]
		public void UpdateExpenseTest()
		{
			var expense = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);

			expenseManager.AddExpense(expense);
			expense.Amount = 500000;
			expenseManager.UpdateExpense(expense);

			var updatedExpense = dataContext.Expense.FirstOrDefault(x => x.ExpenseId == expense.ExpenseId);

			Assert.IsNotNull(updatedExpense);
			Assert.AreEqual(expense.Amount, updatedExpense.Amount);
		}

		[TestMethod]
		public void GetAllExpensesTest()
		{
			var expense1 = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);
			var expense2 = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);
			var expense3 = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);

			var expectedExpenseCount = 3;
			expenseManager.AddExpense(expense1);
			expenseManager.AddExpense(expense2);
			expenseManager.AddExpense(expense3);

			var allExpenses = expenseManager.GetAllExpenses(expenseStory.ExpenseStoryId);

			Assert.IsNotNull(allExpenses);
			Assert.AreEqual(expectedExpenseCount, allExpenses.Count());
 
		}

		[TestMethod]
		public void DeleteExpenseTest()
		{
			var expense1 = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);
			var expense2 = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);
			var expense3 = GetNewMockExpense(collaborator.CollaboratorId, expenseStory.ExpenseStoryId);

			expenseManager.AddExpense(expense1);
			expenseManager.AddExpense(expense2);
			expenseManager.AddExpense(expense3);

			expenseManager.DeleteExpense(expense1.ExpenseId);
			var isExists = dataContext.Expense.Any(x => x.ExpenseId == expense1.ExpenseId);
			Assert.IsFalse(isExists);
		}
	}
}

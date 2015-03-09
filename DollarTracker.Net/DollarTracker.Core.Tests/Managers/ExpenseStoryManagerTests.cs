using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DollarTracker.EF;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Repository;
using DollarTracker.EF;

namespace DollarTracker.Core.Tests.Managers
{
	/// <summary>
	/// Summary description for ExpenseManagerTests
	/// </summary>
	[TestClass]
	public class ExpenseStoryManagerTests : TestEnvironmentBase
	{
		private User user;
		private ExpenseStoryRepository expenseStoryRepository;
		private ExpenseStoryManager expenseStoryManager;
		public ExpenseStoryManagerTests()
		{
			user = GetNewMockUser();
			new UserManager(new UserRepository(dbFactory), unitOfWork).AddUser(user);
			expenseStoryRepository = new ExpenseStoryRepository(dbFactory);
			expenseStoryManager = new ExpenseStoryManager(expenseStoryRepository, unitOfWork);
		}


		[TestMethod]
		public void TestMethod1()
		{
			//
			// TODO: Add test logic here
			//
		}

		private void GetNewMockExpenseStory()
		{
			var expenseStory = new ExpenseStory
			{

			};
		}

	}
}

using DollarTracker.Core.Managers;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		public void AddNewPersonalExpenseStoryTest()
		{
			var expectedExpenseStory = GetNewMockPersonalExpenseStory(user.UserId);
			expenseStoryManager.AddExpenseStory(expectedExpenseStory);

			var actualExpenseStory = dataContext.ExpenseStory.FirstOrDefault(x => x.ExpenseStoryId == expectedExpenseStory.ExpenseStoryId);

			Assert.IsNotNull(actualExpenseStory);
			Assert.AreEqual(expectedExpenseStory.GetHashCode(), actualExpenseStory.GetHashCode());
		}

		[TestMethod]
		public void AddNewSharedExpenseStoryTest()
		{
			var expectedExpenseStory = GetNewMockSharedExpenseStory(user.UserId);
			expenseStoryManager.AddExpenseStory(expectedExpenseStory);

			var actualExpenseStory = dataContext.ExpenseStory.FirstOrDefault(x => x.ExpenseStoryId == expectedExpenseStory.ExpenseStoryId);

			Assert.IsNotNull(actualExpenseStory);
			Assert.AreEqual(expectedExpenseStory.GetHashCode(), actualExpenseStory.GetHashCode());
		}

		[TestMethod]
		public void UpdateExpenseStoryTest()
		{
			var expenseStory = GetNewMockPersonalExpenseStory(user.UserId);
			expenseStoryManager.AddExpenseStory(expenseStory);

			expenseStory.Income = Faker.NumberFaker.Number(5000000);
			expenseStoryManager.UpdateExpenseStory(expenseStory);
			var actualExpenseStory = dataContext.ExpenseStory.FirstOrDefault(x => x.ExpenseStoryId == expenseStory.ExpenseStoryId);


			Assert.IsNotNull(actualExpenseStory);
			Assert.AreEqual(expenseStory.Income, actualExpenseStory.Income);
		}

		[TestMethod]
		public void GetAllExpenseStoryTest()
		{
			var mockUser = GetNewMockUser();
			var exp1 = GetNewMockPersonalExpenseStory(mockUser.UserId);
			var exp2 = GetNewMockPersonalExpenseStory(mockUser.UserId);
			var exp3 = GetNewMockSharedExpenseStory(mockUser.UserId);
			var exp4 = GetNewMockSharedExpenseStory(mockUser.UserId);
			int expectedStoryCount = 4;

			expenseStoryManager.AddExpenseStory(exp1);
			expenseStoryManager.AddExpenseStory(exp2);
			expenseStoryManager.AddExpenseStory(exp3);
			expenseStoryManager.AddExpenseStory(exp4);

			var stories = expenseStoryManager.GetAllExpenseStories(mockUser.UserId);

			Assert.IsNotNull(stories);
			Assert.AreEqual(expectedStoryCount, stories.Count());
		}

		[TestMethod]
		public void GetTopNExpenseStoriesTests()
		{
			var mockUser = GetNewMockUser();
			var exp1 = GetNewMockPersonalExpenseStory(mockUser.UserId);
			var exp2 = GetNewMockPersonalExpenseStory(mockUser.UserId);
			var exp3 = GetNewMockSharedExpenseStory(mockUser.UserId);
			var exp4 = GetNewMockSharedExpenseStory(mockUser.UserId);
			int expectedStoryCount = 2;

			expenseStoryManager.AddExpenseStory(exp1);
			expenseStoryManager.AddExpenseStory(exp2);
			expenseStoryManager.AddExpenseStory(exp3);
			expenseStoryManager.AddExpenseStory(exp4);

			var stories = expenseStoryManager.GetTopNExpenseStories(mockUser.UserId, expectedStoryCount);

			Assert.IsNotNull(stories);
			Assert.AreEqual(expectedStoryCount, stories.Count());
		}

		[TestMethod]
		public void DeleteExpenseStoryTests()
		{
			var mockUser = GetNewMockUser();
			var exp1 = GetNewMockPersonalExpenseStory(mockUser.UserId);
			var exp2 = GetNewMockPersonalExpenseStory(mockUser.UserId);

			expenseStoryManager.AddExpenseStory(exp1);
			expenseStoryManager.AddExpenseStory(exp2);

			expenseStoryManager.DeleteExpenseStory(exp1.ExpenseStoryId);

			var story = expenseStoryRepository.Get(x => x.ExpenseStoryId == exp1.ExpenseStoryId);
			Assert.IsNull(story);
		}

		
	}
}

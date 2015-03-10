using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DollarTracker.EF;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using System.Linq;

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
			var expectedExpenseStory = GetNewMockPersonalExpenseStory();
			expenseStoryManager.AddExpenseStory(expectedExpenseStory);

			var actualExpenseStory = dataContext.ExpenseStory.FirstOrDefault(x => x.ExpenseStoryId == expectedExpenseStory.ExpenseStoryId);

			Assert.IsNotNull(actualExpenseStory);
			Assert.AreEqual(expectedExpenseStory.GetHashCode(), actualExpenseStory.GetHashCode());
		}

		[TestMethod]
		public void AddNewSharedExpenseStoryTest()
		{
			var expectedExpenseStory = GetNewMockSharedExpenseStory();
			expenseStoryManager.AddExpenseStory(expectedExpenseStory);

			var actualExpenseStory = dataContext.ExpenseStory.FirstOrDefault(x => x.ExpenseStoryId == expectedExpenseStory.ExpenseStoryId);

			Assert.IsNotNull(actualExpenseStory);
			Assert.AreEqual(expectedExpenseStory.GetHashCode(), actualExpenseStory.GetHashCode());
		}

		[TestMethod]
		public void UpdateExpenseStoryTest()
		{
			var expenseStory = GetNewMockPersonalExpenseStory();
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
			var exp1 = GetNewMockPersonalExpenseStory();
			var exp2 = GetNewMockPersonalExpenseStory();
			var exp3 = GetNewMockSharedExpenseStory();
			var exp4 = GetNewMockSharedExpenseStory();
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
			var exp1 = GetNewMockPersonalExpenseStory();
			var exp2 = GetNewMockPersonalExpenseStory();
			var exp3 = GetNewMockSharedExpenseStory();
			var exp4 = GetNewMockSharedExpenseStory();
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
			var exp1 = GetNewMockPersonalExpenseStory();
			var exp2 = GetNewMockPersonalExpenseStory();

			expenseStoryManager.AddExpenseStory(exp1);
			expenseStoryManager.AddExpenseStory(exp2);

			expenseStoryManager.DeleteExpenseStory(exp1.ExpenseStoryId);

			var story = expenseStoryRepository.Get(x => x.ExpenseStoryId == exp1.ExpenseStoryId);
			Assert.IsNull(story);
		}

		private ExpenseStory GetNewMockPersonalExpenseStory()
		{
			return GetNewMockExpenseStory("Personal");
		}

		private ExpenseStory GetNewMockSharedExpenseStory()
		{
			return GetNewMockExpenseStory("Shared");
		}

		private ExpenseStory GetNewMockExpenseStory(string expenseStoryTypeId)
		{
			var expenseStory = new ExpenseStory
			{
				ExpenseStoryId = Guid.NewGuid().ToString("N").Substring(0, 20),
				ExpenseStoryTypeId = expenseStoryTypeId,
				CreatedBy = user.UserId,
				StartDt = DateTime.UtcNow,
				EndDt = DateTime.UtcNow.AddDays(10),
				CreatedUtcDt = DateTime.UtcNow,
				Income = (float)Faker.NumberFaker.Number(10000, 100000),
				Budget = (float)Faker.NumberFaker.Number(1000, 10000)
			};
			return expenseStory;
		}
	}
}

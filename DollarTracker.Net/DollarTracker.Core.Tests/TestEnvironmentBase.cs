using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.Core.Utils;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Tests
{
	public class TestEnvironmentBase
	{
		protected readonly IDbFactory dbFactory;
		protected readonly DollarTrackerEntities dataContext;
		protected readonly UnitOfWork unitOfWork;
		public TestEnvironmentBase()
		{
			dbFactory = new DbFactory();
			dataContext = dbFactory.Get();
			unitOfWork = new UnitOfWork(dbFactory);
			DataMock.Seed();
		}

		protected User GetNewMockUser()
		{
			return new User
			{
				UserId = Guid.NewGuid(),
				Password = Guid.NewGuid().ToString("N"),
				PasswordSalt = Guid.NewGuid(),
				Username = Guid.NewGuid().ToString("N").Substring(0, 10),
				Email = Guid.NewGuid().ToString("N").Substring(0,10) +  "@test.com",
				CreatedDtUtc = DateTime.UtcNow,
				Status = true
			};
		}

		protected ExpenseStory GetNewMockPersonalExpenseStory(Guid userId)
		{
			return GetNewMockExpenseStory("Personal", userId);
		}

		protected ExpenseStory GetNewMockSharedExpenseStory(Guid userId)
		{
			return GetNewMockExpenseStory("Shared", userId);
		}

		protected ExpenseStory GetNewMockExpenseStory(string expenseStoryTypeId, Guid userId)
		{
			var expenseStory = new ExpenseStory
			{
				ExpenseStoryId = Guid.NewGuid().ToString("N").Substring(0, 20),
				ExpenseStoryTypeId = expenseStoryTypeId,
				CreatedBy = userId,
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

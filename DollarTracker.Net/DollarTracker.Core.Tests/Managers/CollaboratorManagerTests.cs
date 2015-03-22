using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using DollarTracker.EF;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Repository;

namespace DollarTracker.Core.Tests.Managers
{
	[TestFixture]
	public class CollaboratorManagerTests : TestEnvironmentBase
	{
		private readonly User user;
		private readonly ExpenseStory expenseStory;
		private readonly CollaboratorManager collaboratorMgr;

		public CollaboratorManagerTests()
		{
			user = GetNewMockUser();
			expenseStory = GetNewMockPersonalExpenseStory(user.UserId);
			new ExpenseStoryManager(new ExpenseStoryRepository(dbFactory), unitOfWork).AddExpenseStory(expenseStory);
			collaboratorMgr = new CollaboratorManager(new CollaboratorRepository(dbFactory), unitOfWork);
		}

		[Test]
		public void AddCollaboratorTest()
		{
			
			var expectedCollaborator = GetNewMockCollaborator(user.UserId, expenseStory.ExpenseStoryId);
			collaboratorMgr.AddCollaborator(expectedCollaborator);

			var actualCollaborator = dataContext.Collaborator.FirstOrDefault(c => c.CollaboratorId == expectedCollaborator.CollaboratorId);

			Assert.IsNotNull(actualCollaborator, "Collaborator should not be null");
			Assert.AreEqual(actualCollaborator.CollaboratorId, expectedCollaborator.CollaboratorId);
			Assert.AreEqual(actualCollaborator.GetHashCode(), expectedCollaborator.GetHashCode(), "actual and expected collaborator hash code should be equal");
		}

		[Test]
		public void AddExistingCollaboratorShouldNotAddToDbTest()
		{
			int expectedCount = 1;

			var collaborator = GetNewMockCollaborator(user.UserId, expenseStory.ExpenseStoryId);
			collaboratorMgr.AddCollaborator(collaborator);

			var actualCount = dataContext.Collaborator.Where(c => c.CollaboratorId == collaborator.CollaboratorId).ToList().Count;

			Assert.AreEqual(expectedCount, actualCount, "Collaborator count should be equal to 1");
		}

		[Test]
		public void UpdateCollaboratorTest()
		{
			var collaborator = GetNewMockCollaborator(user.UserId, expenseStory.ExpenseStoryId);
			collaborator.Status = false;
			var expectedStatus = true;

			collaboratorMgr.AddCollaborator(collaborator);
			collaborator.Status = expectedStatus;
			collaboratorMgr.UpdateCollaborator(collaborator);

			var actualStatus = dataContext.Collaborator.First(c => c.CollaboratorId == collaborator.CollaboratorId).Status;

			Assert.AreEqual(expectedStatus, actualStatus);
		}

		[Test]
		public void GetAllCollaboratorsTest()
		{
			var expenseStory1 = GetNewMockPersonalExpenseStory(user.UserId);
			var collaborator1 = GetNewMockCollaborator(user.UserId, expenseStory1.ExpenseStoryId);
			var collaborator2 = GetNewMockCollaborator(user.UserId, expenseStory1.ExpenseStoryId);
			var collaborator3 = GetNewMockCollaborator(user.UserId, expenseStory1.ExpenseStoryId);
			var collaboratorMgr = new CollaboratorManager(new CollaboratorRepository(dbFactory), unitOfWork);
			new ExpenseStoryManager(new ExpenseStoryRepository(dbFactory), unitOfWork).AddExpenseStory(expenseStory1);
			var expectedCollaboratorsCount = 3;

			collaboratorMgr.AddCollaborator(collaborator1);
			collaboratorMgr.AddCollaborator(collaborator2);
			collaboratorMgr.AddCollaborator(collaborator3);

			var actualCollaboratorsCount = collaboratorMgr.GetAllCollaborators(expenseStory1.ExpenseStoryId).Count();

			Assert.AreEqual(expectedCollaboratorsCount, actualCollaboratorsCount);
		}

		[Test]
		public void GetAllCollaboratorsShouldReturnZeroWithInvalidStoryIdTest()
		{
			var expenseStory1 = GetNewMockPersonalExpenseStory(user.UserId);
			var expectedCollaboratorsCount = 0;
			
			var actualCollaboratorsCount = collaboratorMgr.GetAllCollaborators(expenseStory1.ExpenseStoryId).Count();

			Assert.AreEqual(expectedCollaboratorsCount, actualCollaboratorsCount);
		}

		[Test]
		public void DeleteCollaboratorTest()
		{
			var expenseStory1 = GetNewMockPersonalExpenseStory(user.UserId);
			var collaborator1 = GetNewMockCollaborator(user.UserId, expenseStory1.ExpenseStoryId);
			var collaboratorMgr = new CollaboratorManager(new CollaboratorRepository(dbFactory), unitOfWork);
			new ExpenseStoryManager(new ExpenseStoryRepository(dbFactory), unitOfWork).AddExpenseStory(expenseStory1);

			collaboratorMgr.AddCollaborator(collaborator1);
			collaboratorMgr.DeleteCollaborator(collaborator1.CollaboratorId);

			var isCollaboratorExists = dataContext.Collaborator.Any(c => c.CollaboratorId == collaborator1.CollaboratorId);

			Assert.IsFalse(isCollaboratorExists);
		}
	}
}

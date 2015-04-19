using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DollarTracker.Core.Repository;
using DollarTracker.Core.Managers;
using DollarTracker.EF;
using System.Linq;

namespace DollarTracker.Core.Tests.Managers
{
	[TestClass]
	public class UserManagerTests : TestEnvironmentBase
	{
		private UserRepository userRepository;
		private UserManager userManager;
		public UserManagerTests()
		{
			userRepository = new UserRepository(dbFactory);
			userManager = new UserManager(userRepository, unitOfWork);
		}
	
		[TestMethod]
		public void AddUserTest()
		{
			var expectedUser = GetNewMockUser();

			userManager.AddUser(expectedUser);
			var actualUser = dataContext.User.First(x => x.UserId == expectedUser.UserId);

			Assert.IsNotNull(actualUser);
			Assert.AreEqual(expectedUser.GetHashCode(), actualUser.GetHashCode());
		}

		[TestMethod]
		public void AddExistingUserWithSameEmailShouldNotAddToDbTest()
		{
			var user = GetNewMockUser();
			int expectedCount = 1;
			
			userManager.AddUser(user);
			user.UserId = Guid.NewGuid().ToString("N");
			user.Username = Guid.NewGuid().ToString("N").Substring(0, 10);
			userManager.AddUser(user);
			int actualCount = dataContext.User.Where(x => x.Email == user.Email).Count();

			Assert.AreEqual(expectedCount, actualCount);
		}

		[TestMethod]
		public void AddExistingUserWithSameUserNameShouldNotAddToDbTest()
		{
			var user = GetNewMockUser();
			int expectedCount = 1;

			userManager.AddUser(user);
			user.UserId = Guid.NewGuid().ToString("N");
			user.Email = Guid.NewGuid().ToString("N").Substring(0, 10) + "@test.com";
			userManager.AddUser(user);
			int actualCount = dataContext.User.Where(x => x.Username == user.Username).Count();

			Assert.AreEqual(expectedCount, actualCount);
		}

		[TestMethod]
		public void GetUserViaUserIdTests()
		{
			var expectedUser = GetNewMockUser();
			userManager.AddUser(expectedUser);

			var actualUser = userManager.GetUserViaUserId(expectedUser.UserId);

			Assert.IsNotNull(actualUser);
			Assert.AreEqual(expectedUser.GetHashCode(), actualUser.GetHashCode());
		}

		[TestMethod]
		public void GetUserViaUserIdNotExistsTests()
		{
			var inValidUserId = Guid.NewGuid().ToString("N");

			var actualUser = userManager.GetUserViaUserId(inValidUserId);
			Assert.IsNull(actualUser);
		}

		[TestMethod]
		public void GetUserViaEmailTests()
		{
			var expectedUser = GetNewMockUser();
			userManager.AddUser(expectedUser);

			var actualUser = userManager.GetUserViaEmail(expectedUser.Email);

			Assert.IsNotNull(actualUser);
			Assert.AreEqual(expectedUser.GetHashCode(), actualUser.GetHashCode());
		}

		[TestMethod]
		public void GetUserViaEmailNotExistsTests()
		{
			var inValidEmail = Guid.NewGuid().ToString("N").Substring(0,10) + "@test.com";

			var actualUser = userManager.GetUserViaEmail(inValidEmail);
			Assert.IsNull(actualUser);
		}

		[TestMethod]
		public void GetUserViaUsernameTests()
		{
			var expectedUser = GetNewMockUser();
			userManager.AddUser(expectedUser);

			var actualUser = userManager.GetUserViaUsername(expectedUser.Username);

			Assert.IsNotNull(actualUser);
			Assert.AreEqual(expectedUser.GetHashCode(), actualUser.GetHashCode());
		}

		[TestMethod]
		public void GetUserViaUsernameNotExistsTests()
		{
			var inValidUsername = Guid.NewGuid().ToString("N").Substring(0,10);

			var actualUser = userManager.GetUserViaUsername(inValidUsername);
			Assert.IsNull(actualUser);
		}
	}
}

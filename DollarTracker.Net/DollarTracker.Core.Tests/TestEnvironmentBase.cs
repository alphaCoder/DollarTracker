using DollarTracker.Core.Infrastructure;
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
	}
}

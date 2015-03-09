using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Utils
{
	public class DataMock
	{
		private static DbFactory dbFactory = new DbFactory();
		private static IUnitOfWork unitOfWork = new UnitOfWork(dbFactory);
		private static bool isCalled = false;
		private static readonly object seedLock = new object();
		public static void Seed()
		{
			if (!isCalled)
			{
				lock (seedLock)
				{
					if (!isCalled)
					{
						isCalled = true;
						SeedExpenseStoryType();
					}
				}
			}
		}

		private static void SeedExpenseStoryType()
		{
			try
			{
				var expenseStoryTypeRepo = new ExpenseStoryTypeRepository(dbFactory);
				var personalType = new ExpenseStoryType
				{
					ExpenseStoryTypeId = "Personal",
					Description = "Personal story type, usually individual person"
				};

				var shareType = new ExpenseStoryType
				{
					ExpenseStoryTypeId = "Shared",
					Description = "Shared story type, usually two or more people"
				};

				expenseStoryTypeRepo.Add(personalType);
				expenseStoryTypeRepo.Add(shareType);
				unitOfWork.Save();
			}
			catch (Exception e)
			{
				
				throw;
			}
		}
	}
}

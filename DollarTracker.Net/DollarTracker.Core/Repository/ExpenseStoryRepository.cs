using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Repository
{
	public interface IExpenseStoryRepository : IRepository<ExpenseStories>
	{

	}
	public class ExpenseStoryRepository: RepositoryBase<ExpenseStories>, IExpenseStoryRepository
	{
		public ExpenseStoryRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}

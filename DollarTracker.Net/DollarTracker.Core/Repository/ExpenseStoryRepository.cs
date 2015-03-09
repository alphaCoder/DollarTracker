using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Repository
{
	public interface IExpenseStoryRepository : IRepository<ExpenseStory>
	{

	}
	public class ExpenseStoryRepository: RepositoryBase<ExpenseStory>, IExpenseStoryRepository
	{
		public ExpenseStoryRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
namespace DollarTracker.Core.Repository
{
	public interface IExpenseStoryRepository : IRepository<ExpenseStoryType> { }

	public class ExpenseStoryTypeRepository : RepositoryBase<ExpenseStoryType>, IExpenseStoryRepository
	{
		public ExpenseStoryTypeRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}

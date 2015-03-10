using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Repository
{
	public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
	{

	}
	public class ExpenseCategoryRepository : RepositoryBase<ExpenseCategory>, IExpenseCategoryRepository
	{
		public ExpenseCategoryRepository(IDbFactory dbFactory) : base(dbFactory) { }
	}
}

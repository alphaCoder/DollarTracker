using DollarTracker.Core.Infrastructure;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Repository
{
	public interface IExpenseRepository : IRepository<Expense>
	{

	}
	public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
	{
		public ExpenseRepository(IDbFactory dbFactory) : base(dbFactory) { }
	}
}

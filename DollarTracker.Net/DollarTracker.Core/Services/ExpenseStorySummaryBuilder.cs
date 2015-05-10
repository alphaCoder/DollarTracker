using DollarTracker.Core.Managers;
using DollarTracker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Services
{
	public interface IExpenseStorySummaryBuilder
	{
		ExpenseStorySummary Build(string expenseStoryId);
	}
	public class ExpenseStorySummaryBuilder : IExpenseStorySummaryBuilder
	{
		private readonly IExpenseManager expenseManager;
		
		public ExpenseStorySummaryBuilder(IExpenseManager expenseManager)
		{
			this.expenseManager = expenseManager;
		}

		public ExpenseStorySummary Build(string expenseStoryId)
		{
			var expenseStorySummary = new ExpenseStorySummary();
			expenseStorySummary.ExpensesStats = expenseManager.GetExpensesStats(expenseStoryId);
			expenseStorySummary.TotalExpenseCount = expenseManager.TotalExpenseCount(expenseStoryId);
			expenseStorySummary.TotalExpenses = expenseStorySummary.ExpensesStats.Sum(x=>x.Value);
			return expenseStorySummary;
		}
	}
}

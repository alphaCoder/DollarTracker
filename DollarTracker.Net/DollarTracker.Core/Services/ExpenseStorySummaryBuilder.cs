using DollarTracker.Core.Managers;
using DollarTracker.Core.Models;
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

		public override ExpenseStorySummary Build(string expenseStoryId)
		{
			var expenseStorySummary = new ExpenseStorySummary();
			expenseStorySummary.ExpensesStatsByCategory = expenseManager.GetExpensesStats(expenseStoryId);
			expenseStorySummary.TotalExpenseCount = expenseManager.TotalExpenseCount(expenseStoryId);
			
			return expenseStorySummary;
		}
	}
}

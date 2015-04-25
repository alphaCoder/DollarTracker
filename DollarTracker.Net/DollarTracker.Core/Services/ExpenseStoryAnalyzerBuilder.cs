using DollarTracker.Core.Managers;
using DollarTracker.Core.Models;
using DollarTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Services
{
	public interface IExpenseStoryAnalyzer
	{

	}

	public abstract class ExpenseStoryAnalyzerBase: IExpenseStoryAnalyzer
	{
		public abstract ExpenseStorySummary Build(string expenseStoryId);
	}

	public class ExpenseStoryAnalyzerBuilder : ExpenseStoryAnalyzerBase
	{
		private readonly IExpenseStoryManager expenseStoryManager;
		private readonly IExpenseManager expenseManager;

		public ExpenseStoryAnalyzerBuilder(IExpenseStoryManager expenseStoryManager, IExpenseManager expenseManager)
		{
			this.expenseStoryManager = expenseStoryManager;
			this.expenseManager = expenseManager;
		}

		public override ExpenseStorySummary Build(string expenseStoryId)
		{
			var expenseStorySummary = new ExpenseStorySummary();

			//expenseStorySummary.TotalExpenseCount = 
			return expenseStorySummary;
		}
	}
}

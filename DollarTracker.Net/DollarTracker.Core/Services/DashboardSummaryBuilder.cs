using DollarTracker.Core.Managers;
using DollarTracker.Common;
using DollarTracker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Services
{
	public interface IDashboardSummaryBuilder
	{
		DashboardSummary Build(string userId);
	}

	public class DashboardSummaryBuilder : IDashboardSummaryBuilder
	{
		private readonly IExpenseStoryManager expenseStoryManager;
		private readonly IExpenseStorySummaryBuilder expenseStorySummaryBuilder;
		public DashboardSummaryBuilder(IExpenseStoryManager expenseStoryManager, IExpenseStorySummaryBuilder expenseStorySummaryBuilder)
		{
			this.expenseStoryManager = expenseStoryManager;
			this.expenseStorySummaryBuilder = expenseStorySummaryBuilder;
		}

		public DashboardSummary Build(string userId)
		{
			var stories = expenseStoryManager.GetAllExpenseStories(userId);
			var dashboardSummary = new DashboardSummary();
			List<ExpenseStorySummary> eSummaries = new List<ExpenseStorySummary>();
			foreach (var story in stories)
			{
				var expenseStorySummary = expenseStorySummaryBuilder.Build(story.ExpenseStoryId);
				expenseStorySummary.ExpenseStory = story;
				eSummaries.Add(expenseStorySummary);
			}

			dashboardSummary.ExpenseStorySummaries = eSummaries;

			return dashboardSummary;
		}
	}
}

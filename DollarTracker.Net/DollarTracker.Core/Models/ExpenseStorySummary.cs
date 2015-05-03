using DollarTracker.Common;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Models
{
	public class ExpenseStorySummary
	{
		public ExpenseStorySummary()
		{
			ExpensesStats = new List<ExpensesStat>();
		}
		public int TotalExpenseCount { get; set; }
		public ExpenseStory ExpenseStory { get; set; }
		public List<ExpensesStat> ExpensesStats { get; set; } //using it for charts
		public double TotalExpenses { get; set; }
	}
}

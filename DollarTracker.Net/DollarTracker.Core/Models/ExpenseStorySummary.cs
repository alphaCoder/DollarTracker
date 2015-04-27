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
			ExpensesStatsByCategory = new Dictionary<string, double>();
		}
		public int TotalExpenseCount { get; set; }
		public ExpenseStory ExpenseStory { get; set; }
		public Dictionary<string, double> ExpensesStatsByCategory { get; set; } //using it for charts
	}
}

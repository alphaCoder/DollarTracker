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
		public int TotalExpenseCount { get; set; }
		public ExpenseStory ExpenseStory { get; set; }
		public IEnumerable<Expense> Expenses { get; set; } //usually top x
		public Dictionary<string, double> ExpensesByCategory { get; set; } //using it for charts
	}
}

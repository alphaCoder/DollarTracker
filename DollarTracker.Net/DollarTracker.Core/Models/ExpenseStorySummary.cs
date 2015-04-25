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
			ExpenseCategorByCount = new Dictionary<string, int>();
		}
		public int TotalExpenseCount { get; set; }
		public ExpenseStory ExpenseStory { get; set; }
		public Dictionary<string, double> ExpensesByCategory { get; set; } //using it for charts
		public Dictionary<string, int> ExpenseCategorByCount { get; set; } 
	}
}

using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Managers
{
	public interface IExpenseManager
	{
		void AddExpense(Expense e);
		void UpdateExpense(Expense e);
		
		IEnumerable<Expense> GetAllExpenses(string storyId);
		IEnumerable<Expense> GetAllExpenses(string storyId, Guid collaboratorId);
		IEnumerable<Expense> GetTopNExpense(string storyId, int n);
		Dictionary<string, double> GetExpensesStats(string storyId);
		int TotalExpenseCount(string storyId);
				
		void DeleteExpense(string expenseId);
		void SaveExpense();
	}
	public class ExpenseManager : IExpenseManager
	{
		private readonly IExpenseRepository expenseRepository;
		private readonly IUnitOfWork unitOfWork;

		public ExpenseManager(IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
		{
			this.expenseRepository = expenseRepository;
			this.unitOfWork = unitOfWork;
		}

		public void AddExpense(Expense e)
		{
			if (!expenseRepository.Any(x => x.ExpenseId == e.ExpenseId))
			{
				expenseRepository.Add(e);
				SaveExpense();
			}
		}

		public void UpdateExpense(Expense e)
		{
			var existingExpense = expenseRepository.Get(x => x.ExpenseId == e.ExpenseId && x.ExpenseStoryId == e.ExpenseStoryId);
			if (existingExpense != null)
			{
				existingExpense.Amount = e.Amount;
				if(!string.IsNullOrEmpty(e.CustomExpenseCategoryId)) {
					existingExpense.CustomExpenseCategoryId = e.CustomExpenseCategoryId;
				}
				expenseRepository.Update(existingExpense);
				SaveExpense();
			}
		}

		public IEnumerable<Expense> GetAllExpenses(string storyId)
		{
			return expenseRepository.GetMany(x => x.ExpenseStoryId == storyId);
		}
		public IEnumerable<Expense> GetAllExpenses(string storyId, Guid collaboratorId)
		{
			return expenseRepository.GetMany(x => x.ExpenseStoryId == storyId && x.CollaboratorId == collaboratorId);
		}

		public IEnumerable<Expense> GetTopNExpense(string storyId, int n)
		{
			return expenseRepository.GetMany(x => x.ExpenseStoryId == storyId).Take(n);
		}

		public int TotalExpenseCount(string storyId)
		{
			return expenseRepository.Count(e => e.ExpenseStoryId == storyId);
		}

		public void DeleteExpense(string expenseId)
		{
			if(expenseRepository.Any(x=>x.ExpenseId == expenseId)){
				expenseRepository.Delete(x=>x.ExpenseId == expenseId);
				SaveExpense();
			}
		}

		public void SaveExpense()
		{
			unitOfWork.Save();
		}


		public Dictionary<string, double> GetExpensesStats(string storyId)
		{
			var stats = GetAllExpenses(storyId).GroupBy(e => e.ExpenseCategoryId).Select(c => new
			{
				key = c.Key,
				value = c.Sum(x=>x.Amount)
			}).ToDictionary(pair=>pair.key, pair=>pair.value);

			return stats;
		}
	}
}

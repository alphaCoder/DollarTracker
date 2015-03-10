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
		void DeleteExpense(string expenseId);
		void SaveExpenses();
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
				SaveExpenses();
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
				SaveExpenses();
				//? todo: need to determine if modify any other fields.
			}
		}

		public IEnumerable<Expense> GetAllExpenses(string storyId)
		{
			return expenseRepository.GetMany(x => x.ExpenseStoryId == storyId);
		}

		public void DeleteExpense(string expenseId)
		{
			if(expenseRepository.Any(x=>x.ExpenseId == expenseId)){
				expenseRepository.Delete(x=>x.ExpenseId == expenseId);
				SaveExpenses();
			}
		}

		public void SaveExpenses()
		{
			unitOfWork.Save();
		}
	}
}

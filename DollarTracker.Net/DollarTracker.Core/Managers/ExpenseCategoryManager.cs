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
	public interface IExpenseCategoryManager
	{
		IEnumerable<ExpenseCategory> GetAllExpenseCategories();  
		//todo: other methods
	}
	public class ExpenseCategoryManager : IExpenseCategoryManager
	{
		private IUnitOfWork unitOfWork;
		private IExpenseCategoryRepository expenseCategoryRepository;

		public ExpenseCategoryManager(IExpenseCategoryRepository expenseCategoryRepository, IUnitOfWork unitOfWork)
		{
			this.expenseCategoryRepository = expenseCategoryRepository;
			this.unitOfWork = unitOfWork;
		}
		public IEnumerable<ExpenseCategory> GetAllExpenseCategories()
		{
			return expenseCategoryRepository.GetAll();
		}
	}
}

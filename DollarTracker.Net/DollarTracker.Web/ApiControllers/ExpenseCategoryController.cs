using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DollarTracker.Core.Managers;
using DollarTracker.EF;

namespace DollarTracker.Web.ApiControllers
{
    public class ExpenseCategoryController : ApiController
    {
		private IExpenseCategoryManager expenseCategoryManager;
		public ExpenseCategoryController(IExpenseCategoryManager expenseCategoryManager)
		{
			this.expenseCategoryManager = expenseCategoryManager;
		}
		public IEnumerable<ExpenseCategory> Get()
		{
			var categories = expenseCategoryManager.GetAllExpenseCategories();
			return categories;
		}
    }
}

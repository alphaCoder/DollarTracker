using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DollarTracker.Core.Managers;
using DollarTracker.EF;
using DollarTracker.Web.Utils;

namespace DollarTracker.Web.ApiControllers
{
	[DTJwtApiAuthorization]
    public class ExpenseCategoryController : DollarTrackerBaseController
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

using DollarTracker.Common;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Utils;
using DollarTracker.EF;
using DollarTracker.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DollarTracker.Web.ApiControllers
{
	[DTJwtApiAuthorization]
	public class ExpenseController : DollarTrackerBaseController
	{
		private readonly IExpenseManager expenseManager;
		private readonly ICollaboratorManager collaboratorManager;
		public ExpenseController(IExpenseManager expenseManager, ICollaboratorManager collaboratorManager)
		{
			this.expenseManager = expenseManager;
			this.collaboratorManager = collaboratorManager;
		}
		//[Route("api/getallexpenses")]
		public DollarTrackerResponse<IEnumerable<Expense>> Get(string id)
		{
			var response = new DollarTrackerResponse<IEnumerable<Expense>>();
			try
			{
				var expenses = expenseManager.GetAllExpenses(id);
				response.Data = expenses;
				response.Success = true;
			}
			catch (Exception e)
			{
				//todo: log the server error
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}

		[Route("api/addExpense")]
		public DollarTrackerResponse<Expense> Post(Expense expense)
		{
			var response = new DollarTrackerResponse<Expense>();
			try
			{
				//for now by passing all the validations. todo: fix it
				var collaborator = collaboratorManager.GetCollaborator(UserClaim.UserId, expense.ExpenseStoryId);
				expense.ExpenseId = UniqueKeyGenerator.DatePrefixShortKey();
				expense.CreatedUtcDt = DateTime.UtcNow;
				expense.CollaboratorId = collaborator.CollaboratorId; 
				expenseManager.AddExpense(expense); //todo: need to validate the expense content
				response.Data = expense;

			}
			catch (Exception e)
			{
				//todo: log the server error
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}

		[HttpDelete]
		public DollarTrackerResponse<Expense> Delete(string id)
		{
			var response = new DollarTrackerResponse<Expense>();
			try
			{
				expenseManager.DeleteExpense(id);
				response.Success = true;
			}
			catch (Exception e)
			{
				//todo: log the server error
				response.Success = false;
				response.Message = "Unknown Server error";
			}
			return response;
		}

		//todo: delete, update an expense
	}
}

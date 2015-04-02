﻿using DollarTracker.Common;
using DollarTracker.Core.Managers;
using DollarTracker.Core.Utils;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DollarTracker.Web.ApiControllers
{
	public class ExpenseController : ApiController
	{
		private readonly IExpenseManager expenseManager;
		public ExpenseController(IExpenseManager expenseManager)
		{
			this.expenseManager = expenseManager;
		}
		//[Route("api/getallexpenses")]
		public DollarTrackerResponse<IEnumerable<Expense>> Get(string id)
		{
			var response = new DollarTrackerResponse<IEnumerable<Expense>>();
			try
			{
				Guid userId = Guid.Parse("DB6B3AA4-8981-45D1-8E67-11B94FF0DF85");  //todo: will get from the user session
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
				Guid userId = Guid.Parse("DB6B3AA4-8981-45D1-8E67-11B94FF0DF85");  //todo: will get from the user session
				expense.ExpenseId = UniqueKeyGenerator.DatePrefixShortKey();
				expense.CreatedUtcDt = DateTime.UtcNow;
				expense.CollaboratorId = userId; //todo: this need to be replaced by calling a method.
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